using System.Data.Common;
using AutoMapper;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class NutritionDb : Db<NutritionGetResponseModel>, INutritionDb
    {
        private readonly IMapper _mapper;
        public NutritionDb(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, 
            IMapper mapper,
            MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
            _mapper = mapper;
        }

        public async Task<List<NutritionGetResponseModel>> GetNutritionByNutritionType ( string nutritionType, int userId)
        {
            string sql;
            List<NutritionGetResponseModel> alimentacaoList;
            sql = $@"
                SELECT 
	                n.id AS NutritionId,
                    n.nutrition_type AS NutritionType,
                    n.time AS Time,
                    n.title AS Title,
                    n.user_id AS UserId,
                    n.is_active AS IsActive
                FROM nutrition n
                WHERE n.user_id = {userId}
                AND n.nutrition_type = '{nutritionType}'
                ";

            await Connect();
            await Query(sql);
            alimentacaoList = await GetQueryResultList();
            await Disconnect();
            return alimentacaoList;
        }
        
        public async Task<List<NutritionGetResponseModel>> GetNutritionByUserId ( int userId)
        {
            string sql;
            List<NutritionGetResponseModel> alimentacaoList;
            sql = $@"
             SELECT 
	            n.id AS NutritionId,
                n.nutrition_type AS NutritionType,
                n.time AS Time,
                n.time AS Title,
                n.user_id AS UserId,
                n.is_active AS IsActive
            FROM nutrition n
            WHERE n.user_id = {userId};
                ";

            await Connect();
            await Query(sql);
            alimentacaoList = await GetQueryResultList();
            await Disconnect();
            return alimentacaoList;
        }
 
        
        protected override NutritionGetResponseModel Mapper(DbDataReader reader)
        {
            return _mapper.Map<NutritionGetResponseModel>(reader);
        }
    }
}
