using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface INutritionRepository
    {
        int Create(Nutrition food);
        void Update(Nutrition food);
    }
}
