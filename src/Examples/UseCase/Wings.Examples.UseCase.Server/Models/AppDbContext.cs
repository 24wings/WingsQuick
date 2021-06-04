using Microsoft.EntityFrameworkCore;

namespace Wings.Api.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }

        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
                   : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Role>()
                            //主语this，拥有Children
                            .HasMany(x => x.Menus);


        }



    }
}