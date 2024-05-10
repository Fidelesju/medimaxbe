using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IStatusDispenserService
    {
        Task<int> CriandoOuAtualizandoStatusDispenser(StatusDispenserCreateRequestModel request);
        Task<StatusDispenserListaResponseModel> BuscandoStatusDispenser ( int treatmentId );
        Task<int> CalculadoraQuantidadeCaixasTratamento ( CalculadoraCaixasRequestModel request );
    }
}
