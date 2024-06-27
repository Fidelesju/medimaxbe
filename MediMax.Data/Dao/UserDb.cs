using System.Data.Common;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Enums;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class UserDb : Db<UsuarioResponseModel>, IUsuarioDb
    {
        public UserDb(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, 
            MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
        }

        public async Task<UsuarioResponseModel> GetUserById(int userId)
        {
            string sql;
            UsuarioResponseModel user;
            sql = $@"
                 SELECT 
                     u.id_usuario AS UserId,
                     u.nome AS Name,
                     u.email AS Email,
                     u.esta_ativo AS IsActive,
                     u.id_tipo_usuario as TypeUser,
		             u.id_proprietario as OwnerId
                 FROM usuarios u 
                 WHERE u.id_usuario = {userId}
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

       public async Task<int> UpdateUser( UsuarioUpdateRequestModel request )
        {
            string sql;
           
            sql = $@"
                UPDATE usuarios u 
                SET  u.nome = '{request.UserName}',
	                 u.email = '{request.Email}',
	                 u.id_tipo_usuario = {request.TypeUserId}
                 WHERE u.id_usuario = {request.UserId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return request.UserId;
        }
        
        public async Task<int> DesativarUsuario( int userId )
        {
            string sql;
            int user;
            sql = $@"
                 UPDATE usuarios u 
                    SET u.esta_ativo = 0
                 WHERE u.id_usuario = {userId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return userId;
        }
        public async Task<int> ReativarUsuario ( int userId )
        {
            string sql;
            int user;
            sql = $@"
                 UPDATE usuarios u 
                    SET u.esta_ativo = 1
                 WHERE u.id_usuario = {userId}
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
                 UPDATE usuarios u 
                    SET u.senha = '{password}'
                 WHERE u.id_usuario = {userId}
                ;";

            await Connect();
            await Query(sql);
            await Disconnect();
            return true;
        }

        public async Task<UsuarioResponseModel> GetUserByEmail(string email)
        {
            string sql;
            UsuarioResponseModel user;
            sql = $@"
                     SELECT 
                         u.id_usuario AS UserId,
                         u.nome AS Name,
                         u.email AS Email,
                         u.esta_ativo AS IsActive,
                         u.id_tipo_usuario as TypeUser,
		                 u.id_proprietario as OwnerId
                     FROM usuarios u 
                     WHERE u.email like '%{email}%'
                     AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

        public async Task<UsuarioResponseModel> GetUserByName ( string name )
        {
            string sql;
            UsuarioResponseModel user;
            sql = $@"
                 SELECT 
                     u.id_usuario AS UserId,
                     u.nome AS Name,
                     u.email AS Email,
                     u.esta_ativo AS IsActive,
                     u.id_tipo_usuario as TypeUser,
		             u.id_proprietario as OwnerId
                 FROM usuarios u 
                 WHERE u.nome like '%{name}%'
                 AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

        public async Task<List<UsuarioResponseModel>> GetUserByTypeUser ( int typeUser )
        {
            string sql;
            List<UsuarioResponseModel> user;
            sql = $@"
                    SELECT 
                        u.id_usuario AS UserId,
                        u.nome AS Name,
                        u.email AS Email,
                        u.esta_ativo AS IsActive,
                        u.id_tipo_usuario as TypeUser,
		                u.id_proprietario as OwnerId
                    FROM usuarios u 
                    WHERE u.id_tipo_usuario = {typeUser}
                    AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultList();
            await Disconnect();
            return user;
        }

        public async Task<List<UsuarioResponseModel>> GetUserByOwnerOfTypeUser( int typeUser, int ownerId )
        {
            string sql;
            List<UsuarioResponseModel> user;
            sql = $@"
                    SELECT 
                        u.id_usuario AS UserId,
                        u.nome AS Name,
                        u.email AS Email,
                        u.esta_ativo AS IsActive,
                        u.id_tipo_usuario as TypeUser,
		                u.id_proprietario as OwnerId
                    FROM usuarios u 
                    WHERE u.id_tipo_usuario = {typeUser}
                    AND u.id_proprietario = {ownerId}
                    AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultList();
            await Disconnect();
            return user;
        }

        public async Task<List<UsuarioResponseModel>> GetUserByOwner( int ownerId )
        {
            string sql;
            List<UsuarioResponseModel> user;
            sql = $@"
                    SELECT 
                        u.id_usuario AS UserId,
                        u.nome AS Name,
                        u.email AS Email,
                        u.esta_ativo AS IsActive,
                        u.id_tipo_usuario as TypeUser,
		                u.id_proprietario as OwnerId
                    FROM usuarios u 
                    WHERE u.id_proprietario = {ownerId}
                    AND  u.esta_ativo = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultList();
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
            user.TypeUser = Convert.ToInt32(reader["TypeUser"]);
            user.OwnerId = Convert.ToInt32(reader["OwnerId"]);
            return user;
        }
    }
}
