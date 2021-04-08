using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();//Metodun varsa argümanlarını listeye çeviriyoruz.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

            if (_cacheManager.IsAdd(key))//Bellekte bu key ile ilgili bir cache var mı kontrolü yapıyoruz.
            {
                invocation.ReturnValue = _cacheManager.Get(key);//cache'de olduğunda geri döndürüyoruz.
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);//Gelen verileri cache' ekle


            //23. satır:
            //invocation.Method.ReflectedType ile namespace'i alıyoruz, .FullName ile namespace + class'ın ismi.
            //Örneğin Business.Concrete.IProductService
            //invocation.Method.Name ile metodun ismini alıyoruz. Örneğin GetAll
            //Tamamının çıktısı şekilde olur: Business.Concrete.IProductService.GetAll

            //25. satır:
            //Burada metodun parametreleri varsa her bir parametreyi ekliyoruz. Yoksa null olarak ekle.
            //Bunun çıktısı şu şekilde olur: Business.Concrete.IProductService.GetAll(1)

            //29. satır:
            //invocation.ReturnValue: Metodu hiç çalıştırmadan geri dön. Manuel olarak bir return oluşturuyoruz.
            //Yani asıl metodumuzdaki return'a girmeyecek. Cache'da olduğundan dolayı, veri cache'den geliyor

            //32. satır
            //Cache yoksa metodu çalıştır. Metod çalışınca veritabanına gidecek verileri
            //veritabanında çekecek.
        }
    }
}
