using DotNet5Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotNet5Website.Middlewares
{
    public class TestLifetimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IOperationTransient _transientOperation;
        private readonly IOperationSingleton _singletonOperation;

        public TestLifetimeMiddleware(RequestDelegate next, ILogger<TestLifetimeMiddleware> logger,
            IOperationTransient transientOperation,
            IOperationSingleton singletonOperation,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _transientOperation = transientOperation;
            _singletonOperation = singletonOperation;
            _serviceScopeFactory = serviceScopeFactory;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            IOperationScoped scopedOperation)
        {
            _logger.LogInformation("Transient: " + _transientOperation.OperationId);
            _logger.LogInformation("Scoped: " + scopedOperation.OperationId);
            _logger.LogInformation("Singleton: " + _singletonOperation.OperationId);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedOperation2 = scope.ServiceProvider.GetService<IOperationScoped>();

                _logger.LogInformation("Scoped2: " + scopedOperation2.OperationId);
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedOperation3 = scope.ServiceProvider.GetService<IOperationScoped>();

                _logger.LogInformation("Scoped3: " + scopedOperation3.OperationId);
            }

            await _next(context);

            _logger.LogInformation("Transient: " + _transientOperation.OperationId);
            _logger.LogInformation("Scoped: " + scopedOperation.OperationId);
            _logger.LogInformation("Singleton: " + _singletonOperation.OperationId);
        }
    }
}
