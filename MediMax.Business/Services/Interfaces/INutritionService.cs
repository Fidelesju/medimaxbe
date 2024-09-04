using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface INutritionService
    {
        Task<BaseResponse<int>> CreateNutrition ( NutritionCreateRequestModel request );
        Task<List<NutritionGetResponseModel>> GetNutritionByType ( string nutritionType, int userId );
        Task<BaseResponse<bool>> UpdateNutrition ( NutritionUpdateRequestModel request );
        Task<BaseResponse<bool>> DesactiveNutrition ( NutritionDesativeRequestModel request );
        Task<List<NutritionGetResponseModel>> GetNutritionByUserId ( int userId );
        Task<BaseResponse<bool>> ReactiveNutrition ( NutritionReactiveRequestModel request );
        Task<List<NutritionDetailResponseModel>> GetNutritionDetailsByUserIAndNutritionId ( int nutritionId );
    }
}
