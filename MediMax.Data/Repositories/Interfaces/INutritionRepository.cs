using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface INutritionRepository
    {
        int Create(Nutrition food);
        Task<bool> Update ( NutritionUpdateResponseModel request );
        Task<bool> Reactive ( NutritionReactiveRequestModel request );
        Task<bool> Desactive ( NutritionDesativeRequestModel request );
    }
}
