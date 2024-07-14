using System.Data.Common;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class UserDb : Db<UserResponseModel>, IUserDb
    {
        public UserDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, 
            MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<UserResponseModel> GetUserById(int userId)
        {
            string sql;
            UserResponseModel user;
            sql = $@"
                 SELECT 
                     u.id_User AS UserId,
                     u.nome AS Name,
                     u.email AS Email,
                     u.esta_ativo AS IsActive,
                     u.id_tipo_User as TypeUser,
		             u.id_proprietario as OwnerId
                 FROM Users u 
                 WHERE u.id_User = {userId}
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

       public async Task<int> UpdateUser( UserUpdateRequestModel request )
        {
            string sql;
           
            sql = $@"
                UPDATE Users u 
                SET  u.nome = '{request.UserName}',
	                 u.email = '{request.Email}',
	                 u.id_tipo_User = {request.TypeUserId}
                 WHERE u.id_User = {request.UserId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return request.UserId;
        }
        
        public async Task<int> DesativarUser( int userId )
        {
            string sql;
            int user;
            sql = $@"
                 UPDATE Users u 
                    SET u.esta_ativo = 0
                 WHERE u.id_User = {userId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return userId;
        }
        public async Task<int> ReativarUser ( int userId )
        {
            string sql;
            int user;
            sql = $@"
                 UPDATE Users u 
                    SET u.esta_ativo = 1
                 WHERE u.id_User = {userId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return userId;
        }
        public async Task<bool> AlterarSenha( int userId, string password )
        {
            string sql;
            sql = $@"
                 UPDATE Users u 
                    SET u.senha = '{password}'
                 WHERE u.id_User = {userId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return true;
        }

        public async Task<UserResponseModel> GetUserByEmail(string email)
        {
            string sql;
            UserResponseModel user;
            sql = $@"
                     SELECT 
                         u.id_User AS UserId,
                         u.nome AS Name,
                         u.email AS Email,
                         u.esta_ativo AS IsActive,
                         u.id_tipo_User as TypeUser,
		                 u.id_proprietario as OwnerId
                     FROM Users u 
                     WHERE u.email like '%{email}%'
                     AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

        public async Task<UserResponseModel> GetUserByName ( string name )
        {
            string sql;
            UserResponseModel user;
            sql = $@"
                 SELECT 
                     u.id_User AS UserId,
                     u.nome AS Name,
                     u.email AS Email,
                     u.esta_ativo AS IsActive,
                     u.id_tipo_User as TypeUser,
		             u.id_proprietario as OwnerId
                 FROM Users u 
                 WHERE u.nome like '%{name}%'
                 AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

        public async Task<List<UserResponseModel>> GetUserByTypeUser ( int typeUser )
        {
            string sql;
            List<UserResponseModel> user;
            sql = $@"
                    SELECT 
                        u.id_User AS UserId,
                        u.nome AS Name,
                        u.email AS Email,
                        u.esta_ativo AS IsActive,
                        u.id_tipo_User as TypeUser,
		                u.id_proprietario as OwnerId
                    FROM Users u 
                    WHERE u.id_tipo_User = {typeUser}
                    AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultList();
            await Disconnect();
            return user;
        }

        public async Task<List<UserResponseModel>> GetUserByOwnerOfTypeUser( int typeUser, int ownerId )
        {
            string sql;
            List<UserResponseModel> user;
            sql = $@"
                    SELECT 
                        u.id_User AS UserId,
                        u.nome AS Name,
                        u.email AS Email,
                        u.esta_ativo AS IsActive,
                        u.id_tipo_User as TypeUser,
		                u.id_proprietario as OwnerId
                    FROM Users u 
                    WHERE u.id_tipo_User = {typeUser}
                    AND u.id_proprietario = {ownerId}
                    AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultList();
            await Disconnect();
            return user;
        }

        public async Task<List<UserResponseModel>> GetUserByOwner( int ownerId )
        {
            string sql;
            List<UserResponseModel> user;
            sql = $@"
                    SELECT 
                        u.id_User AS UserId,
                        u.nome AS Name,
                        u.email AS Email,
                        u.esta_ativo AS IsActive,
                        u.id_tipo_User as TypeUser,
		                u.id_proprietario as OwnerId
                    FROM Users u 
                    WHERE u.id_proprietario = {ownerId}
                    AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultList();
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
            user.TypeUser = Convert.ToInt32(reader["TypeUser"]);
            user.OwnerId = Convert.ToInt32(reader["OwnerId"]);
            return user;
        }
    }
}
