using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface INutritionDetailRepository
    {
        int Create(NutritionDetail food);
        Task<bool> Update ( NutritionDetailResponseModel request );
        Task<bool> Delete ( NutritionDetailDeleteRequestModel request );
    }
}
