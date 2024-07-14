using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class TreatmentRepository : Repository<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Treatment Treatments)
        {
            DbSet.Add(Treatments);
            Context.SaveChanges();
            return Treatments.Id;
        }

        public void Update(Treatment Treatments)
        {
            DbSet.Update(Treatments);
            Context.SaveChanges();
        }

        public async Task<bool> Delete ( int medication_id, int treatment_id )
        {
            string sql = @"
                    UPDATE tratamento
                    SET esta_ativo = 0
                    WHERE remedio_id = {0} AND id = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, medication_id, treatment_id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
    }
}
