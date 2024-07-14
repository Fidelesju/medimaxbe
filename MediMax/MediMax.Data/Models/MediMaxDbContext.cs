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
        public DbSet<User> Users { get; set; }
        public DbSet<Treatment> Treatment { get; set; }
        public DbSet<Medicamentos> Medicamentos { get; set; }
        public DbSet<Alimentacao> Alimentacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.id_User);

            // Outras configurações do modelo

            base.OnModelCreating(modelBuilder);
        }
    }
}
