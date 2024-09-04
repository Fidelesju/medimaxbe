using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.ResponseModels;
using MediMax.Data.RequestModels;

namespace MediMax.Data.Repositories
{
    public class NutritionRepository : Repository<Nutrition>, INutritionRepository
    {
        public NutritionRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Nutrition alimentacao)
        {
            DbSet.Add(alimentacao);
            Context.SaveChanges();
            return alimentacao.Id;
        }

        public async Task<bool> Update ( NutritionUpdateResponseModel request )
        {
            string sql = @"
                UPDATE nutrition n
                SET n.nutrition_type = {0},
                    n.time = {1},
                    n.title = {2}
                WHERE n.id = {3}
                AND n.user_id = {4}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, request.Nutrition_Type, request.Time, request.Title, request.Id, request.User_Id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
        
        public async Task<bool> Desactive ( NutritionDesativeRequestModel request )
        {
            string sql = @"
                UPDATE nutrition n
                SET n.is_active = 0
                WHERE n.id = {0}
                AND n.user_id = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, request.Id, request.User_Id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
        
        public async Task<bool> Reactive ( NutritionReactiveRequestModel request )
        {
            string sql = @"
                UPDATE nutrition n
                SET n.is_active = 1
                WHERE n.id = {0}
                AND n.user_id = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, request.Id, request.User_Id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
    }
}
