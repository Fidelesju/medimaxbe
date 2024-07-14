using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace MediMax.Data.Repositories
{
    public class MedicamentoRepository : Repository<Medicamentos>, IMedicationRepository
    {
        public MedicamentoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Medicamentos medicine)
        {
            DbSet.Add(medicine);
            Context.SaveChanges();
            return medicine.id;
        }

        public void Update(Medicamentos medicine)
        {
            DbSet.Update(medicine);
            Context.SaveChanges();
        }


        public async Task<bool> Delete ( int medication_id, int user_id )
        {
            string sql = @"
                    UPDATE medicamentos
                    SET esta_ativo = 0
                    WHERE id = {0} AND usuarioId = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, medication_id, user_id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }

    }
}
