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
            //单表树状结构
            modelBuilder.Entity<Menu>()
                //主语this，拥有Children
                .HasMany(x => x.Children)
                //主语Children，每个Child拥有一个Parent
                .WithOne(x => x.Parent)
                //主语Children，每个Child的外键是ParentId
                .HasForeignKey(x => x.ParentId)
                //这里必须是非强制关联，否则报错：Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
                .OnDelete(DeleteBehavior.ClientSetNull);

modelBuilder.Entity<Role>()
                //主语this，拥有Children
                .HasMany(x => x.Menus);


        }



    }
}