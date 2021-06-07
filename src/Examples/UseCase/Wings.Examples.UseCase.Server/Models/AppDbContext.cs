using Microsoft.EntityFrameworkCore;
using Wings.Examples.UseCase.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wings.Examples.UseCase.Server.Models
{
    public class AppDbContext : IdentityDbContext<RbacUser,RbacRole,int>
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permission> Permissions { get; set; }


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


        }



    }
}