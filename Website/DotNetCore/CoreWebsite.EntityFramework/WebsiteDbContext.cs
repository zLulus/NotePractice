using System;
using System.Collections.Generic;
using System.Text;
using CoreWebsite.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebsite.EntityFramework
{
    public class WebsiteDbContext : DbContext
    {

        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options):base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityComment> ActivityComments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //导航属性
            modelBuilder.Entity<ActivityComment>()
                .HasOne(p => p.Activity)
                .WithMany(b => b.ActivityComments)
                .HasForeignKey(p => p.ActivityId);
            modelBuilder.Entity<Activity>()
                .HasMany(x => x.ActivityComments)
                .WithOne(x => x.Activity)
                .HasForeignKey(x => x.ActivityId);
        }
    }
}
