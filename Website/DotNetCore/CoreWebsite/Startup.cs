using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using CoreWebsite.EntityFramework;
using AutoMapper;
using CoreWebsite.EntityFramework.Models.EntityRelationTest;
using CoreWebsite.EntityFramework.Dtos.EntityRelationTest;
using CoreWebsite.EntityFramework.Dtos.TreeTest;
using CoreWebsite.EntityFramework.Models.TreeTest;

namespace CoreWebsite
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
            //数据库配置
            //参考资料：https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db
            services.AddEntityFrameworkSqlServer().AddDbContext<WebsiteDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            services.AddMvc();

            SetAutoMapper();
        }

        public void SetAutoMapper()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Student, StudentDto>();
                cfg.CreateMap<AdmissionRecord, AdmissionRecordDto>();
                cfg.CreateMap<Class, ClassDto>();
                cfg.CreateMap<StudentTeacherRelationship, StudentTeacherRelationshipDto>();
                cfg.CreateMap<Teacher, TeacherDto>();
                cfg.CreateMap<TreeNode, TreeNodeDto>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), @"Views")),
                        RequestPath = new PathString("/Views"),
                        ContentTypeProvider = new FileExtensionContentTypeProvider(
                            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                            {
                                { ".js", "application/javascript" },
                                { ".css", "text/css" },
                                { ".html", "text/html" },
                            })
                }
             );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
