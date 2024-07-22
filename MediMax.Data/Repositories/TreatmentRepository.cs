using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.ResponseModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> Update ( TreatmentResponseModel request )
        {
            string sql = @"
                     UPDATE treatment t
                         SET t.name_medication = {1}, 
                         t.medication_quantity = {2}, 
                         t.start_time = {3}, 
                         t.treatment_interval_hours = {4}, 
                         t.treatment_interval_days = {5}, 
                         t.dietary_recommendations = {6}, 
                         t.observation = {7},
                         t.medication_id = {8},
                         t.continuous_use = {9}
                     WHERE t.id = {10}
                     AND t.user_id = {11}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, request.Name_Medication, request.Medication_Quantity, request.Start_Time, request.Treatment_Interval_Hours, request.Treatment_Interval_Days, request.Dietary_Recommendations, request.Observation, request.Continuous_Use, request.Id, request.User_Id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
       
        public async Task<bool> Desactive ( int user_id, int treatment_id )
        {
            string sql = @"
                    UPDATE treatment t
                         SET t.is_active = 0
                     WHERE t.id = {0}
                     AND t.user_id = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql,  treatment_id, user_id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
        
        public async Task<bool> Reactive ( int user_id, int treatment_id )
        {
            string sql = @"
                     UPDATE treatment t
                         SET t.is_active = 1
                     WHERE t.id = {0}
                     AND t.user_id = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, treatment_id, user_id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
    }
}
