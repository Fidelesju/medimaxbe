using AutoMapper;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.ResponseModels;
using System.Data.Common;

namespace MediMax.Data.Dao
{
    public class UserDb : Db<UserResponseModel>, IUserDb
    {
        private readonly IMapper _mapper;
        public UserDb(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, 
            IMapper mapper,
            MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
            _mapper = mapper;
        }

        public async Task<UserResponseModel> GetUserById(int userId)
        {
            string sql;
            UserResponseModel user;
            sql = $@"
                 SELECT 
                     u.id AS UserId,
                     u.name_user AS Name,
                     u.email AS Email,
                     u.is_active AS IsActive,
                     u.type_user_id AS TypeUser,
		             u.owner_id AS OwnerId
                 FROM user u 
                 WHERE u.id = {userId}
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

       

        public async Task<UserResponseModel> GetUserByEmail(string email)
        {
            string sql;
            UserResponseModel user;
            sql = $@"
                     SELECT 
                         u.id AS UserId,
                         u.name_user AS Name,
                         u.email AS Email,
                         u.is_active AS IsActive,
                         u.type_user_id AS TypeUser,
		                 u.owner_id AS OwnerId
                     FROM user u 
                     WHERE u.email like '%{email}%'
                     AND  u.is_active = 1
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
                     u.id AS UserId,
                     u.name_user AS Name,
                     u.email AS Email,
                     u.is_active AS IsActive,
                     u.type_user_id AS TypeUser,
		             u.owner_id AS OwnerId
                 FROM user u 
                 WHERE u.name_user like '%{name}%'
                 AND  u.is_active = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultObject();
            await Disconnect();
            return user;
        }

        public async Task<List<UserResponseModel>> GetUserByType ( int typeUser )
        {
            string sql;
            List<UserResponseModel> user;
            sql = $@"
                    SELECT 
                         u.id AS UserId,
                         u.name_user AS Name,
                         u.email AS Email,
                         u.is_active AS IsActive,
                         u.type_user_id AS TypeUser,
		                 u.owner_id AS OwnerId
                     FROM user u 
                    WHERE u.type_user_id = {typeUser}
                    AND  u.is_active = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultList();
            await Disconnect();
            return user;
        }

        public async Task<List<UserResponseModel>> GetUserByTypeAndOwnerId( int typeUser, int ownerId )
        {
            string sql;
            List<UserResponseModel> user;
            sql = $@"
                    SELECT 
                         u.id AS UserId,
                         u.name_user AS Name,
                         u.email AS Email,
                         u.is_active AS IsActive,
                         u.type_user_id AS TypeUser,
		                 u.owner_id AS OwnerId
                    FROM user u 
                    WHERE u.type_user_id = {typeUser}
                    AND u.owner_id = {ownerId}
                    AND u.is_active = 1
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
                         u.id AS UserId,
                         u.name_user AS Name,
                         u.email AS Email,
                         u.is_active AS IsActive,
                         u.type_user_id AS TypeUser,
		                 u.owner_id AS OwnerId
                     FROM user u 
                    WHERE u.owner_id = {ownerId}
                    AND u.is_active = 1
                ;";

            await Connect();
            await Query(sql);
            user = await GetQueryResultList();
            await Disconnect();
            return user;
        }

        protected override UserResponseModel Mapper(DbDataReader reader)
        {
            return _mapper.Map<UserResponseModel>(reader);
        }
    }
}
