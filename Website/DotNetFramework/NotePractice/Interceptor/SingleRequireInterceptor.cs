using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using NotePractice.Tools.Async;
using NotePractice.Tools.Caches;
using NotePractice.Tools.ClientInfoProviders;
using NotePractice.Tools.Reflections;

namespace NotePractice.Interceptor
{
    public class SingleRequireInterceptor : IInterceptor
    {
        private readonly MemoryCacheManager _cacheManager;

        public SingleRequireInterceptor(
            MemoryCacheManager cacheManager)
        {
            _cacheManager = cacheManager;

        }

        public void Intercept(IInvocation invocation)
        {
            if (AllowAnonymous(invocation.MethodInvocationTarget, invocation.TargetType))
            {
                var clientInfo = ClientInfoProvider.GetClientInfo();
                var key = invocation.Method.Name + invocation.Method.ReflectedType.Name + clientInfo.ClientIpAddress  + clientInfo.ComputerName;
                var keycache = _cacheManager.GetCache(key);
                if (keycache == null)
                {
                    //3秒内不能重复提交
                    _cacheManager.SetCache(key, DateTime.Now,3);
                    invocation.Proceed();

                    if (invocation.Method.IsAsync())
                    {
                        if (invocation.Method.ReturnType == typeof(Task))
                        {
                            invocation.ReturnValue = AsyncHelper.AwaitTaskWithFinally(
                                (Task)invocation.ReturnValue,
                                exception => _cacheManager.Remove(key)
                            );
                        }
                        else
                        {
                            invocation.ReturnValue = AsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                                invocation.Method.ReturnType.GenericTypeArguments[0],
                                invocation.ReturnValue,
                                exception => _cacheManager.Remove(key)
                            );
                        }
                    }
                    else
                    {
                        _cacheManager.Remove(key);
                    }
                }
                else
                {
                    throw new Exception("请不要重复提交！");
                }
            }
            else
            {
                invocation.Proceed();
            }
        }

        private static bool AllowAnonymous(MemberInfo methodInfo, Type type)
        {
            return ReflectionHelper
                .GetAttributesOfMemberAndType(methodInfo, type)
                .OfType<SingleRequireAttribute>()
                .Any();
        }

        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }

    }
}