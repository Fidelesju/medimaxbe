using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface INutritionDb
    {
        Task<List<NutritionResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals, int userId);
        Task<bool> AlterandoAlimentacao(AlimentacaoUpdateRequestModel request);
        Task<bool> AlterandoDetalheAlimentacao ( string quantidade, string alimento, string unidade_medida, int id );
        Task<bool> DeletandoAlimentacao(int id, int userId );
        Task<NutritionResponseModel> BuscarRefeicoesPorHorario ( int userId );
        Task<List<NutritionResponseModel>> BuscarTodasAlimentacao ( int userId );

    }
}
