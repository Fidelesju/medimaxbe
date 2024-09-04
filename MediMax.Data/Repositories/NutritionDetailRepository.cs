using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.ResponseModels;
using MediMax.Data.RequestModels;

namespace MediMax.Data.Repositories
{
    public class NutritionDetailRepository : Repository<NutritionDetail>, INutritionDetailRepository
    {
        public NutritionDetailRepository ( MediMaxDbContext context) : base(context)
        {
        }

        public int Create( NutritionDetail alimentacao )
        {
            DbSet.Add(alimentacao);
            Context.SaveChanges();
            return alimentacao.Id;
        }

        public async Task<bool> Update ( NutritionDetailResponseModel request )
        {
            string sql = @"
                UPDATE nutrition_detail nd
                 SET nd.unit_measurement = {0},
                     nd.quantity = {1},
                     nd.nutrition = {2}
                 WHERE nd.id = {3}
                 AND nd.nutrition_id = {4}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, request.Unit_Measurement, request.Quantity, request.Nutrition, request.Id, request.Nutrition_Id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }

        public async Task<bool> Delete ( NutritionDetailDeleteRequestModel request )
        {
            string sql = @"
               DELETE FROM nutrition_detail nd 
                WHERE nd.id = {0}
                AND nd.nutrition_id = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, request.Id, request.Nutrition_Id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
    }
}
