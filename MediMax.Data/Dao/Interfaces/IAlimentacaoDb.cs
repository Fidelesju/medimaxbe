using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IAlimentacaoDb
    {
        Task<List<AlimentacaoResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals);
        Task<bool> AlterandoAlimentacao(AlimentacaoUpdateRequestModel request);
        Task<bool> DeletandoAlimentacao(int id);
        Task<AlimentacaoResponseModel> BuscarRefeicoesPorHorario ( );
        Task<List<AlimentacaoResponseModel>> BuscarTodasAlimentacao ( );
    }
}
