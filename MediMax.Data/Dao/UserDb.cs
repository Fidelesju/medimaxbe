using System.Data.Common;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class UserDb : Db<UsuarioResponseModel>, IUsuarioDb
    {
        public UserDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<UsuarioResponseModel> GetUserById(int userId)
        {
            string sql;
            UsuarioResponseModel user;
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

        public async Task<UsuarioResponseModel> GetUserByEmail(string name)
        {
            string sql;
            UsuarioResponseModel user;
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
        protected override UsuarioResponseModel Mapper(DbDataReader reader)
        {
            UsuarioResponseModel user;
            user = new UsuarioResponseModel();
            user.Id = Convert.ToInt32(reader["UserId"]);
            user.Name = Convert.ToString(reader["Name"]);
            user.Email = Convert.ToString(reader["Email"]);
            user.IsActive= Convert.ToInt32(reader["IsActive"]);
            return user;
        }
    }
}
