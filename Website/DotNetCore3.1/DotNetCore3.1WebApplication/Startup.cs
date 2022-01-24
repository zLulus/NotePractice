using DotNetCore3._1WebApplication.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace DotNetCore3._1WebApplication
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //AddControllersWithViews+AddRazorPages
            //services.AddMvc();
            //services.AddMvcCore();
            services.AddControllersWithViews(option => option.EnableEndpointRouting = false)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                });

            services.AddScoped<IStudentRepository, MemoryStudentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.Use(async (context, next) =>
            {
                logger.LogInformation($"传入请求时间:{DateTime.Now}");
                await next();
                logger.LogInformation($"传出响应时间:{DateTime.Now}");
            });

            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new
                    DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 3
                };

                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }

            app.UseRouting();

            app.UseAuthorization();

            //支持wwwroot目录下的Index.htm、Index.html、default.htm、default.html   
            //DefaultFilesMiddleware -> DefaultFilesOptions
            app.UseDefaultFiles();
            //允许访问静态文件  eg./images/th.png
            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
