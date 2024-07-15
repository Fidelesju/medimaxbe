using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace MediMax.Data.Repositories
{
    public class MedicamentoRepository : Repository<Medication>, IMedicationRepository
    {
        public MedicamentoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Medication medicine)
        {
            DbSet.Add(medicine);
            Context.SaveChanges();
            return medicine.Id;
        }

        public void Update(Medication medicine)
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
        
        public async Task<bool> Update ( MedicationResponseModel request )
        {
            string sql = @"
                 UPDATE medication m
                   SET m.name_medication = {0}, m.expiration_date = {1}, m.package_quantity = {2}, m.dosage = {3}
                WHERE m.id = {4}
                AND m.user_id = {5}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, request.medicine_name, request.expiration_date, request.package_quantity, request.dosage, request.id, request.user_id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
         public async Task<bool> Desactive ( int medication_id, int user_id )
        {
            string sql = @"
                 UPDATE medication m
                   SET m.is_active = 0
                WHERE m.id = {0}
                AND m.user_id = {1}
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
        public async Task<bool> Reactive ( int medication_id, int user_id )
        {
            string sql = @"
                  UPDATE medication m
                   SET m.is_active = 1
                WHERE m.id = {0}
                AND m.user_id = {1}
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
