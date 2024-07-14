using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface INutritionService
    {
        Task<int> CreateNutrition(NutritionCreateRequestModel request);
        Task<List<NutritionResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals, int userId );
        Task<bool> AlterandoAlimentacao(AlimentacaoUpdateRequestModel request);
        Task<bool> DeletandoAlimentacao(int id, int userId );
        Task<NutritionResponseModel> BuscarRefeicoesPorHorario ( int userId );
    }
}
