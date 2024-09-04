using System.Data.Common;
using AutoMapper;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class AuthDb : Db<LoginResponseModel>, IAuthDb
    {
        private readonly IMapper _mapper;

        public AuthDb(
            IMapper mapper,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnviroment, 
            MediMaxDbContext dbContext) : base(configuration,hostingEnviroment, dbContext)
        {
            _mapper = mapper;
        }
        public async Task<LoginResponseModel> AuthenticateUser(string email, string password)
        {
            string sql;
            LoginResponseModel loginResponseModel;
            sql = $@"
                    SELECT 
                      u.id AS UserId,
                      u.name_user AS Name,
                      u.email AS Email,
                      u.type_user_id AS TypeUserId,
                      u.owner_id AS OwnerId,
                      o.email AS EmailOwner
                      FROM user u
	               INNER JOIN owner o ON o.id = u.owner_id
                   WHERE u.email = '{email}'
                   AND u.password = '{password}'
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
            return _mapper.Map<LoginResponseModel>(reader);
        }
    }
}