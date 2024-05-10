using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IGerenciamentoTratamentoService
    {
        Task<int> CriandoGerenciamentoTratamento(GerencimentoTratamentoCreateRequestModel request);
        Task<List<TratamentoResponseModel>> GetTreatmentByName(string name);
        Task<List<TratamentoResponseModel>> GetIntervalTreatment(string startTime, string finishTime);
        Task<List<HistoricoResponseModel>> BuscarHistoricoGeral();
        Task<List<HistoricoResponseModel>> BuscarHistoricoDataEspecifica(string data);
        Task<List<HistoricoResponseModel>> BuscarHistoricoUltimoAno();
        Task<List<HistoricoResponseModel>> BuscarHistorico60Dias();
        Task<List<HistoricoResponseModel>> BuscarHistorico30Dias();
        Task<List<HistoricoResponseModel>> BuscarHistorico15Dias();
        Task<List<HistoricoResponseModel>> BuscarHistorico7Dias();
        Task<List<HistoricoResponseModel>> BuscarHistoricoNaoTomado();
        Task<List<HistoricoResponseModel>> BuscarHistoricoTomado();
        Task<List<HistoricoResponseModel>> BuscarHistoricoPorMedicamento(string nome);
        Task<bool> BuscarStatusDoUltimoGerenciamento ( );
        Task<string> BuscarUltimoGerenciamento ( );
    }
}
