using System.Data.Common;
using AutoMapper;
using MediMax.Data.ApplicationModels;
using MediMax.Data.Dao.Interfaces;
using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao
{
    public class NutritionDetailDb : Db<NutritionDetailResponseModel>, INutritionDetailDb
    {
        private readonly IMapper _mapper;
        public NutritionDetailDb (
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment, 
            IMapper mapper,
            MediMaxDbContext dbContext) : base(configuration, webHostEnvironment, dbContext)
        {
            _mapper = mapper;
        }

        public async Task<List<NutritionDetailResponseModel>> GetNutritionDetailsByUserIAndNutritionId ( int nutritionId )
        {
            string sql;
            List<NutritionDetailResponseModel> alimentacaoList;
            sql = $@"
                SELECT 
                      nd.id AS NutritionDetailId,
                     nd.nutrition AS Nutrition,
                     nd.quantity AS Quantity,
                     nd.unit_measurement AS UnitMeasurement,
                     nd.nutrition_id AS NutritionId
                 FROM nutrition_detail nd
                 WHERE nd.nutrition_id = {nutritionId}
                ";

            await Connect();
            await Query(sql);
            alimentacaoList = await GetQueryResultList();
            await Disconnect();
            return alimentacaoList;
        }
        
        protected override NutritionDetailResponseModel Mapper (DbDataReader reader)
        {
            return _mapper.Map<NutritionDetailResponseModel>(reader);
        }
    }
}
