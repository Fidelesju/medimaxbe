using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface INutritionDetailDb
    {
        Task<List<NutritionDetailResponseModel>> GetNutritionDetailsByUserIAndNutritionId ( int nutritionId );

    }
}
