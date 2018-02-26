using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer.EasyDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvcCore()
                //将认证服务添加到DI,配置"Bearer"作为默认方案
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                //将IdentityServer访问令牌验证处理程序添加到DI中以供身份验证服务使用
                .AddIdentityServerAuthentication(options =>
                {
                    //用于授权的地址
                    options.Authority = "http://localhost:5000";
                        options.RequireHttpsMetadata = false;
                    //该Api项目对应的IdentityServer的Api资源,与GetApiResources方法里面的Api名称对应
                    options.ApiName = "api1";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //将认证中间件添加到流水线中，以便在对主机的每次呼叫时自动执行认证
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
