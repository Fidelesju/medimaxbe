using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IAlimentacaoService
    {
        Task<int> CriarRefeicoes(AlimentacaoCreateRequestModel request);
        Task<List<AlimentacaoResponseModel>> BuscarAlimentacaoPorTipo(string typeMeals);
    }
}
