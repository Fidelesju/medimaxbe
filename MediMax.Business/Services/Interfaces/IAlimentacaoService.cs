using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IAlimentacaoService
    {
        Task<int> CriarRefeicoes(AlimentacaoCreateRequestModel request);
        Task<List<AlimentacaoResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals, int userId );
        Task<bool> AlterandoAlimentacao(AlimentacaoUpdateRequestModel request);
        Task<bool> DeletandoAlimentacao(int id, int userId );
        Task<AlimentacaoResponseModel> BuscarRefeicoesPorHorario ( int userId );
    }
}
