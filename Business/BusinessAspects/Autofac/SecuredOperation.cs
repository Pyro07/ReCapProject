using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;//IHttpContextAccessor: HttpContext'e bu nesne 
        //aracılığıyla erişiriz. IHttpContextAccessor'ü yalnızca br hizmet içindeki HttpContext'e erişmeniz
        //gerektiğinde kullanmanız gerekir.

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //interface karşılığını veriyoruz.
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            //throw new Exception(Messages.AuthorizationDenied);
            throw new Exception("Yetkiniz bulunmamaktadır");
        }
    }
}
