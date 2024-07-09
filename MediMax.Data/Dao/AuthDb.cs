using System.Data.Common;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class AuthDb : Db<LoginResponseModel>, IAuthDb
    {
        public AuthDb(
            IConfiguration configuration,
            IWebHostEnvironment hostingEnviroment, 
            MediMaxDbContext dbContext) : base(configuration,hostingEnviroment, dbContext)
        {
        }
        public async Task<LoginResponseModel> AuthenticateUser(string email, string login, string password)
        {
            string sql;
            LoginResponseModel loginResponseModel;
            sql = $@"
                    SELECT 
                          u.id_usuario AS UserId,
                             u.nome AS Name,
                             u.email AS Email,
                             u.id_tipo_usuario as TypeUserId
                             FROM usuarios u
                     WHERE u.email = '{email}'
                     OR u.nome = '{login}'
                     AND u.senha = '{password}'
                     LIMIT 1;
                ";
            await Connect();
            await Query(sql);
            loginResponseModel = await GetQueryResultObject();
            await Disconnect();
            return loginResponseModel;
        }

        protected override LoginResponseModel Mapper(DbDataReader reader)
        {
            LoginResponseModel loginResponseModel;
            loginResponseModel = new LoginResponseModel();
            loginResponseModel.Id = Convert.ToInt32(reader["UserId"]);
            loginResponseModel.Name = Convert.ToString(reader["Name"]);
            loginResponseModel.Email = Convert.ToString(reader["Email"]);
            loginResponseModel.TypeUserId = Convert.ToInt32(reader["TypeUserId"]);
            return loginResponseModel;
        }
    }
}