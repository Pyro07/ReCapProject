using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    //MethodInterceptorBaseAttribute sınıfı işlemlerde kullanacağımız attribute yapsının base'i

    //Cross Cutting Concerns(Log, Validation, Cache) uygulamalarımız için yapacağımız işlemleri uygulayabilmek 
    //için Attribute yapısını kullanıyoruz. Örneğin Log işlemini uygulayabilmek için Add metodunun başına 
    //Log Attribute'ünü eklemek gibi.

    //Bu attribute'ün nerelerde uygulanabileceği. Burada Class ve Method'lar da kullanılabilmesini sağlıyoruz
    //AllowMultiple ile attribute'ün birden fazla uygulanabilirliğin sağlıyoruz.
    //Inherited ile inherit edilebilen bir noktada da attribute'ün çalışmasını sağlar
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptorBaseAttribute : Attribute, IInterceptor
    {
        //IInterceptor : Üye müdahalesine izin veren ana DynamicProxy uzantı noktasını sağlar. Yani metodlarda
        //araya girelebileceğimiz yapıyı bize sağlar.

        public int Priority { get; set; }//Öncelik için. Yani uygulanacak işlemlerin hangi sırayla 
        //yapılması istendiğinde

        public virtual void Intercept(IInvocation invocation)
        {
            
        }
    }
}
