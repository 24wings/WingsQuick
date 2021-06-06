using Microsoft.EntityFrameworkCore;
using Wings.Examples.UseCase.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wings.Examples.UseCase.Server.Models
{
    public class AppDbContext : IdentityDbContext<RbacUser>
    {
        //public DbSet<RbacUser> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
                   : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Role>()
            //                //主语this，拥有Children
            //                .HasMany(x => x.Menus);


        }



    }
}