using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace NotePractice.Interceptor
{
    public class SingleRequireInterceptoRegistrar //: IDependencyResolver
    {

        //public static void Initialize(IIocManager iocManager)
        //{
        //    iocManager.Register<SingleRequireInterceptor>(DependencyLifeStyle.Transient);
        //    iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        //}

        //private static void Kernel_ComponentRegistered(string key, IHandler handler)
        //{
        //    if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
        //        if (ShouldIntercept(handler.ComponentModel.Implementation))
        //        {
        //            handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(SingleRequireInterceptor)));
        //        }
        //}

        private static bool ShouldIntercept(Type type)
        {
            if (SelfOrMethodsDefinesAttribute<SingleRequireAttribute>(type))
            {
                return true;
            }

            return false;
        }

        private static bool SelfOrMethodsDefinesAttribute<TAttr>(Type type)
        {
            if (type.GetTypeInfo().IsDefined(typeof(TAttr), true))
            {
                return true;
            }

            return type
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Any(m => m.IsDefined(typeof(TAttr), true));
        }
    }
}