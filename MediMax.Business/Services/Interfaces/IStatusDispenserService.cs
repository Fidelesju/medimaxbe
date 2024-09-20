using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IDispenserStatusService
    {
        Task<int> CriandoOuAtualizandoDispenserStatus ( int treatmentId, int userId, int medicationId );
        Task<DispenserStatusListaResponseModel> BuscandoDispenserStatus ( int treatmentId, int userId );
        Task<int> CalculadoraQuantidadeCaixasTreatment ( CalculadoraCaixasRequestModel request );
    }
}
