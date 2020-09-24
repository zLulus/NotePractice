using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.Api.Filters;
using CoreWebsite.Api.Services;
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
        private readonly IHostingEnvironment _appEnvironment;

        public Startup(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //依赖注入
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

            //依赖注入
            services.AddScoped<ILogService, TxtLogService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //管道、中间件
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //自定义中间件
            app.Use(async (context, next) =>
            {
                //进入管道
                //code executed before the next middleware
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                await next.Invoke();

                //退出管道
                // code executed after the next middleware
                stopWatch.Stop();

                var time = stopWatch.ElapsedMilliseconds;
                var path = $"{_appEnvironment.WebRootPath}\\Log.txt";
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    //获得字节数组
                    byte[] data = System.Text.Encoding.Default.GetBytes($"执行时间:{time}\n");
                    //开始写入
                    fs.Write(data, 0, data.Length);
                    //清空缓冲区、关闭流
                    fs.Flush();
                    fs.Close();
                }
            });

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
