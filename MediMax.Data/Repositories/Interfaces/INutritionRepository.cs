using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface INutritionRepository
    {
        int Create(Alimentacao food);
        void Update(Alimentacao food);
    }
}
