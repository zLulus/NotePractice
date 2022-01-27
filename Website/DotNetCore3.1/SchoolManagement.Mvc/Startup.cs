using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolManagement.Core.Repositories;
using SchoolManagement.EntityFrameworkCore;
using SchoolManagement.EntityFrameworkCore.DataRepositories;

namespace SchoolManagement.Mvc
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
            //使用SQLServer数据库，通过IConfiguration访问去获取数据库连接
            //AddDbContextPool()方法提供了数据库连接池（DbContextPool）
            //AddDbContextPool()方法的性能优于AddDbContext()方法
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("SchoolManagementDBConnection")));

            //AddControllersWithViews+AddRazorPages
            //services.AddMvc();
            //services.AddMvcCore();
            services.AddControllersWithViews(option => option.EnableEndpointRouting = false)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                })
                //协商内容
                //请求头设置为Accept:application/xml
                .AddXmlSerializerFormatters();
            
            //普通仓储
            services.AddScoped<IStudentRepository, MemoryStudentRepository>();
            //泛型仓储
            services.AddTransient(typeof(IRepository<,>), typeof(RepositoryBase<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
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
            else
            {
                //app.UseExceptionHandler("/Error");
                //app.UseStatusCodePages();
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            //支持wwwroot目录下的Index.htm、Index.html、default.htm、default.html   
            //DefaultFilesMiddleware -> DefaultFilesOptions
            app.UseDefaultFiles();
            //允许访问静态文件  eg./images/th.png
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //默认路由
            //｛controller=Home｝/｛action=Index｝/｛id?｝
            //app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            //终结点路由
            //设置路由规则
            //UseEndpoints是一个可以处理跨不同中间件系统（如MVC、 Razor Pages、 Blazor、SignalR和gRPC）的路由系统。通过终结点路由可以使端点相互协作，并使系统比没有相互对话的终端中间件更全面。
            //放置在UseRouting()中间件之后
            //dotnet开发团队推荐使用终结点路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
