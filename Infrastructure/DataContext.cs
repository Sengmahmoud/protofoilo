using core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
   public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext>options)
            :base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortfolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Owner>().HasData(
                new Owner()
                {
                    Id=Guid.NewGuid(),
                    FullName = "Mahmoud Hassan",
                    Avtar = "avatar.jpg",
                    Profile = "Software Development Engineer "

                }
                );
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<PortfolioItem> portfolioItems { get; set; }

    }
}
