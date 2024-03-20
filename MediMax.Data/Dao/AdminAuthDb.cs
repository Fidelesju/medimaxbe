using System.Data.Common;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class AdminAuthDb : Db<LoginAdminResponseModel>, IAdminAuthDb
    {
        public AdminAuthDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<LoginAdminResponseModel> AuthenticateUserAdmin(string email, string password)
        {
            string sql;
            LoginAdminResponseModel user;
            sql = $@"
                SELECT 
	                  SELECT 
                         u.userId AS UserId,
                         u.typeUserId as TypeUserId
                    FROM user u
                    WHERE u.email = '{email}'
                    AND u.password = '{password}'
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

        protected override LoginAdminResponseModel Mapper(DbDataReader reader)
        {
            LoginAdminResponseModel loginAdminResponseModel;
            loginAdminResponseModel = new LoginAdminResponseModel();
            loginAdminResponseModel.TypeUserId = Convert.ToInt32(reader["TypeUserId"]);
            loginAdminResponseModel.UserId = Convert.ToInt32(reader["UserId"]);
            return loginAdminResponseModel;
        }
    }
}
