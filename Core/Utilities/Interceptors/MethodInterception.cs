using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptorBaseAttribute
    {
        //IInvocation invocation : Metoda karışılık gelir. Yani çalıştırmak istediğimiz metoda.

        protected virtual void OnBefore(IInvocation invocation) { }//Metot başlamadan önce
        protected virtual void OnAfter(IInvocation invocation) { }//Metottan bittikten sonra
        protected virtual void OnException(IInvocation invocation, Exception ex) { }//Metotta hata olduğunda
        protected virtual void OnSuccess(IInvocation invocation) { }//Metottan başarı ile tamamlandığında

        public override void Intercept(IInvocation invocation)
        {
            //Burada temel bir try-catch bloğumuz var. 

            var isSuccess = true;

            OnBefore(invocation);

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                isSuccess = false;
                OnException(invocation, ex);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }

            OnAfter(invocation);
        }
    }
}
