using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace NotePractice.Interceptor
{
    public class SingleRequireInterceptor //: IInterceptor
    {
        //private readonly ICacheManager _cacheManager;
        //private readonly IClientInfoProvider _clientInfo;

        //public SingleRequireInterceptor(
        //    ICacheManager cacheManager,
        //    IClientInfoProvider clientInfo)
        //{
        //    _cacheManager = cacheManager;
        //    _clientInfo = clientInfo;

        //}

        //public void Intercept(IInvocation invocation)
        //{
        //    if (AllowAnonymous(invocation.MethodInvocationTarget, invocation.TargetType))
        //    {
        //        var key = invocation.Method.Name + invocation.Method.ReflectedType.Name + _clientInfo.ClientIpAddress + _clientInfo.BrowserInfo + _clientInfo.ComputerName;
        //        var keycache = _cacheManager.GetCache("singleRequireCache").GetOrDefault(key);
        //        if (keycache == null)
        //        {
        //            _cacheManager.GetCache("singleRequireCache").Set(key, 1);
        //            invocation.Proceed();

        //            if (invocation.Method.IsAsync())
        //            {
        //                if (invocation.Method.ReturnType == typeof(Task))
        //                {
        //                    invocation.ReturnValue = AsyncHelper.AwaitTaskWithFinally(
        //                        (Task)invocation.ReturnValue,
        //                        exception => _cacheManager.GetCache("singleRequireCache").Remove(key)
        //                    );
        //                }
        //                else
        //                {
        //                    invocation.ReturnValue = AsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
        //                        invocation.Method.ReturnType.GenericTypeArguments[0],
        //                        invocation.ReturnValue,
        //                        exception => _cacheManager.GetCache("singleRequireCache").Remove(key)
        //                    );
        //                }
        //            }
        //            else
        //            {
        //                _cacheManager.GetCache("singleRequireCache").Remove(key);
        //            }
        //        }
        //        else
        //        {
        //            throw new UserFriendlyException("请不要重复提交！");
        //        }
        //    }
        //    else
        //    {
        //        invocation.Proceed();
        //    }

        //    //#if DEBUG

        //    //            invocation.Proceed();

        //    //#else
        //    //            if (invocation.Method.Name != "L")
        //    //            {
        //    //                var key = invocation.Method.Name + invocation.Method.ReflectedType.Name + _clientInfo.ClientIpAddress + _clientInfo.BrowserInfo + _clientInfo.ComputerName;
        //    //                var keycache = _cacheManager.GetCache("singleRequireCache").GetOrDefault(key);
        //    //                if (keycache == null)
        //    //                {
        //    //                    _cacheManager.GetCache("singleRequireCache").Set(key, 1);
        //    //                    invocation.Proceed();
        //    //                }
        //    //                else
        //    //                {
        //    //                    throw new UserFriendlyException("方法" + invocation.Method.Name + "出现多次访问，联系前端修改");
        //    //                }
        //    //            }
        //    //            else
        //    //            {
        //    //                invocation.Proceed();
        //    //            }

        //    //#endif
        //}

        //private static bool AllowAnonymous(MemberInfo methodInfo, Type type)
        //{
        //    return ReflectionHelper
        //        .GetAttributesOfMemberAndType(methodInfo, type)
        //        .OfType<SingleRequireAttribute>()
        //        .Any();
        //}

        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }

    }
}