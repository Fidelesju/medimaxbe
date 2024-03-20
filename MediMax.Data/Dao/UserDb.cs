using System.Data.Common;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class UserDb : Db<UserResponseModel>, IUserDb
    {
        public UserDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<UserResponseModel> GetUserById(int userId)
        {
            string sql;
            UserResponseModel user;
            sql = $@"
                SELECT 
	                    u.userId AS UserId,
                        u.name AS Name,
                        u.email AS Email,
                        u.isActive AS IsActive
                    FROM user u 
                    WHERE u.userId = {userId}
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

        public async Task<UserResponseModel> GetUserByEmail(string name)
        {
            string sql;
            UserResponseModel user;
            sql = $@"
                SELECT 
                    u.userId AS UserId,
                    u.name AS Name,
                    u.email AS Email,
                    u.isActive AS IsActive
                FROM user u 
                WHERE u.name like '%{name}%'
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }
        protected override UserResponseModel Mapper(DbDataReader reader)
        {
            UserResponseModel user;
            user = new UserResponseModel();
            user.Id = Convert.ToInt32(reader["UserId"]);
            user.Name = Convert.ToString(reader["Name"]);
            user.Email = Convert.ToString(reader["Email"]);
            user.IsActive= Convert.ToInt32(reader["IsActive"]);
            return user;
        }
    }
}
