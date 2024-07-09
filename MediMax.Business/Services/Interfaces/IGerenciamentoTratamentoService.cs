using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IGerenciamentoTratamentoService
    {
        Task<int> CriandoGerenciamentoTratamento(GerencimentoTratamentoCreateRequestModel request);
        Task<List<TratamentoResponseModel>> GetTreatmentByName(string name, int userId );
        Task<List<TratamentoResponseModel>> GetIntervalTreatment(string startTime, string finishTime, int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoGeral ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoDataEspecifica(string data, int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoUltimoAno ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistorico60Dias ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistorico30Dias ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistorico15Dias ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistorico7Dias ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoNaoTomado ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoTomado ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoPorMedicamento(string nome, int userId );
        Task<bool> BuscarStatusDoUltimoGerenciamento ( int userId );
        Task<string> BuscarUltimoGerenciamento ( int userId );
    }
}
