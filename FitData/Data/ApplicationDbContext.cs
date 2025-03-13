using FitCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // 1:1 Trainee And NutritionPlan
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.NutritionPlan)
                .WithOne(p => p.Trainee)
                .HasForeignKey<NutritionPlans>(p => p.TraineeId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1:M Nutritionist And CreatedNutritionPlans
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.CreatedNutritionPlans)
                .WithOne(p => p.Nutritionist)
                .HasForeignKey(p => p.NutritionistId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Level> Levels { get; set; }
        public DbSet<NutritionPlans> nutritionPlans { get; set; }
        public DbSet<Video> Video { get; set; }
    }
}
