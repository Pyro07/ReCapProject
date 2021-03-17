using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptorBaseAttribute>(true).ToList();
            //class'ın attribute'lerini oku ve listele

            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptorBaseAttribute>(true);
            //method'un attribute'lerini oku

            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(c => c.Priority).ToArray();//Çalışma sırasını sıralar ve dizeye çevirir
        }
    }
}
