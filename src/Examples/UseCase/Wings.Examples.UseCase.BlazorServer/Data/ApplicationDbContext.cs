using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wings.Examples.UseCase.BlazorServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRole>()
                            //主语this，拥有Children
                            .HasMany(x => x.Menus)

                            ;

            modelBuilder.Entity<ApplicationRole>()
                            //主语this，拥有Children
                            .HasMany(x => x.Permissions);


        }
    }
}
