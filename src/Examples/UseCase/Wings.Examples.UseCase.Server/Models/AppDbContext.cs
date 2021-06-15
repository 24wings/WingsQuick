using Microsoft.EntityFrameworkCore;
using Wings.Examples.UseCase.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.EntityFramework.Interfaces;
using System;
using IdentityServer4.EntityFramework.Entities;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Extensions;

namespace Wings.Examples.UseCase.Server.Models
{
    public interface BaseEntity
    {
        int Id { get; set; }
    }
    
    public class AppDbContext : IdentityDbContext<RbacUser, RbacRole, int>
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Attr> Attrs { get; set; }

        public DbSet<AttrCategory> AttrCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
                   : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RbacRole>()
                            //主语this，拥有Children
                            .HasMany(x => x.Menus);

            modelBuilder.Entity<RbacRole>()
                            //主语this，拥有Children
                            .HasMany(x => x.Permissions);
            //modelBuilder.Entity<Category>()
            //            //主语this，拥有Children
            //            .HasMany(x => x.Attrs);


        }



    }
}