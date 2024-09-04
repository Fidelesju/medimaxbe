using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface INutritionDb
    {
        Task<List<NutritionGetResponseModel>> GetNutritionByNutritionType ( string nutritionType, int userId );
        Task<List<NutritionGetResponseModel>> GetNutritionByUserId ( int userId );

    }
}
