using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IDetalheAlimentacaoRepository
    {
        int Create( NutritionDetail detalheAlimentacao );
        void Update( NutritionDetail detalheAlimentacao );
    }
}
