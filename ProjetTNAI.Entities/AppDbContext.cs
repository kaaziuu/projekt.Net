using ProjetTNAI.Entities.Configurations;
using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTNAI.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Prowadzacy> Prowadzacy { get; set; }
        public DbSet<Zajecia> Zajecia { get; set; }

        public DbSet<Plan> Plany { get; set; }

        public AppDbContext() : base("AppConnection")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProwadzacyConfiguration());
            modelBuilder.Configurations.Add(new ZajeciaConfiguration());
            modelBuilder.Configurations.Add(new PlanConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
