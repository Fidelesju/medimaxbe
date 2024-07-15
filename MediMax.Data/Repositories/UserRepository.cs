using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(User users)
        {
            DbSet.Add(users);
            Context.SaveChanges();
            return users.Id;
        }

        public void Update(User users)
        {
            DbSet.Update(users);
            Context.SaveChanges();
        }

        public async Task<bool> Desactive ( int id, int owner_id )
        {
            string sql = @"
                UPDATE user u 
                    SET  u.is_active = 0
                WHERE u.id = {0}
                AND u.owner_id = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, id, owner_id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        } 
        
        public async Task<bool> Reactive ( int id, int owner_id )
        {
            string sql = @"
                UPDATE user u 
                    SET  u.is_active = 1
                WHERE u.id = {0}
                AND u.owner_id = {1}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, id, owner_id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
        
        public async Task<bool> Update ( UserResponseModel request )
        {
            string sql = @"
                UPDATE user u 
                SET  u.name_user = {0},
	                 u.email = {1},
	                 u.type_user_id = {2}
                WHERE u.id = {3}
                AND u.owner_id = {4}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, request.Name_User, request.Email, request.Type_User_Id, request.Id, request.Owner_Id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
        
        public async Task<bool> UpdatePassword ( string password, int id, int owner_id )
        {
            string sql = @"
                UPDATE user u 
                    SET u.password = {0}
                 WHERE u.id = {1}
                 AND u.owner_ID = {2}
                ";
            try
            {
                var rowsAffected = await Context.Database.ExecuteSqlRawAsync(sql, password,id, owner_id);
                return rowsAffected > 0;
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Failed to update the database.", exception);
            }
        }
    }
}
