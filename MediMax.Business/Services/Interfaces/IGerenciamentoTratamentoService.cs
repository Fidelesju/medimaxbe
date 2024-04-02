using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IGerenciamentoTratamentoService
    {
        Task<int> CriandoGerenciamentoTratamento(GerencimentoTratamentoCreateRequestModel request);
        Task<List<TratamentoResponseModel>> GetTreatmentByName(string name);
        Task<List<TratamentoResponseModel>> GetIntervalTreatment(string startTime, string finishTime);
    }
}
