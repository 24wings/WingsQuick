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
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Brand> Brands { get; set; }


        public DbSet<Ad> Ads { get; set; }

        public DbSet<AdPosition> AdPositions { get; set; }
        public DbSet<TopicCategory> TopicCategorys { get; set; }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<Good> Goods { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Specification> Specifications { get; set; }
        public DbSet<GoodSpecification> GoodSpecifications { get; set; }
        public DbSet<GoodAttr> GoodAttrs { get; set; }
        public DbSet<GoodIssue> GoodIssues { get; set; }
        public DbSet<GoodGallery> GoodGallerys { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentPicture> CommentPictures { get; set; }

        public DbSet<Collect> Collects { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<WxUser> WxUsers { get; set; }
        public DbSet<WxUserCoupon> WxUserCoupons { get; set; }
        public DbSet<WxUserLevel> WxUserLevels { get; set; }
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