using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class MediMaxDbContext : DbContext
    {
        public MediMaxDbContext(DbContextOptions<MediMaxDbContext> options) : base(options)
        {

        }

        public DbSet<Proprietarios> Proprietarios { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Treatment> Tratamento { get; set; }
        public DbSet<Medicamentos> Medicamentos { get; set; }
        public DbSet<Alimentacao> Alimentacao { get; set; }
        public DbSet<TreatmentManagement> Gerenciamento_Treatment { get; set; }
        public DbSet<HorariosDosagem> Horarios_Dosagem { get; set; }
        public DbSet<DispenserStatus> Status_Dispenser { get; set; }
        public DbSet<DetalheAlimentacao> Detalhe_Alimentacao { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.id_User);

            // Outras configurações do modelo

            base.OnModelCreating(modelBuilder);
        }
    }
}
