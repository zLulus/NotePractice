using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace CoreWebsite.EntityFramework
{
    public class WebsiteContextFactory : IDesignTimeDbContextFactory<WebsiteDbContext>
    {
        public WebsiteContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public WebsiteDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebsiteDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            return new WebsiteDbContext(optionsBuilder.Options);
        }
    }
}
