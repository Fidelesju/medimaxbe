using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class MediMaxDbContext : DbContext
    {
        public MediMaxDbContext(DbContextOptions<MediMaxDbContext> options) : base(options)
        {

        }

        public DbSet<Owner> Owner { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Treatment> Treatment { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Nutrition> Nutrition { get; set; }
        public DbSet<NutritionDetail> Nutrition_Detail { get; set; }
        public DbSet<TreatmentManagement> Treatment_Management { get; set; }
        public DbSet<TimeDosage> Time_Dosage { get; set; }
        public DbSet<StatusDispenser> Status_Dispenser { get; set; }
        public DbSet<Restrition> Restrition { get; set; }
        public DbSet<RestritionDetail> Restrition_Detail { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            // Outras configurações do modelo

            base.OnModelCreating(modelBuilder);
        }
    }
}
