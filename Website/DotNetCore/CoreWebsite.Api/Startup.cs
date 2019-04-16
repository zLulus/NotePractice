using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreWebsite.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //跨域
            services.AddCors();

            //swagger
            //https://www.cnblogs.com/OMango/p/8460092.html
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CoreWebsite.Api", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "CoreWebsite.Api.xml");
                //注释文件
                c.IncludeXmlComments(xmlPath);
                //将某些客户端信息通过header带到服务中，例如token信息，用户信息等（我们项目中就需要header中带上token传递到后端）
                //c.OperationFilter<AddAuthTokenHeaderParameter>();
            });
            services.AddMvcCore().AddApiExplorer();

            //在 .NET Core 中运行 JavaScript
            //https://www.cnblogs.com/stulzq/p/10535310.html
            services.AddNodeServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //跨域  注意顺序
            app.UseCors(builder =>
            {
                builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //swagger
            app.UseMvcWithDefaultRoute();
            app.UseSwagger(c =>
            {
            });
            app.UseSwaggerUI(c =>
            {
                c.ShowExtensions();
                c.ValidatorUrl(null);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebsite.Api V1");
            });

            app.Run(async (context) =>
            {
                context.Response.Redirect("/swagger");
            });
        }
    }
}
