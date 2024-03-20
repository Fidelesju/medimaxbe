using System.Data.Common;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class OwnerAuthDb : Db<LoginOwnerResponseModel>, IOwnerAuthDb
    {
        public OwnerAuthDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<LoginOwnerResponseModel> AuthenticateUserOwner(string email, string password)
        {
            string sql;
            LoginOwnerResponseModel user;
            sql = $@"
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

        protected override LoginOwnerResponseModel Mapper(DbDataReader reader)
        {
            LoginOwnerResponseModel loginOwnerResponseModel;
            loginOwnerResponseModel = new LoginOwnerResponseModel();
            loginOwnerResponseModel.TypeUserId = Convert.ToInt32(reader["TypeUserId"]);
            loginOwnerResponseModel.UserId = Convert.ToInt32(reader["UserId"]);
            return loginOwnerResponseModel;
        }
    }
}
