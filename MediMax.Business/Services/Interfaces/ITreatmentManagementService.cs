using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface ITreatmentManagementService
    {
        Task<int> CreateTreatmentManagement(TreatmentManagementCreateRequestModel request);
        Task<List<TreatmentResponseModel>> BuscarTreatmentPorMedicamentoId(int medicineId, int userId );
        Task<List<TreatmentResponseModel>> GetIntervalTreatment(string startTime, string finishTime, int userId );
        Task<List<TreatmentManagementResponseModel>> GetAllHistoric ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoDataEspecifica(string data, int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoUltimoAno ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistorico60Dias ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistorico30Dias ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistorico15Dias ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistorico7Dias ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoNaoTomado ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoTomado ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoPorMedicamento(string nome, int userId );
        Task<bool> BuscarStatusDoUltimoGerenciamento ( int userId );
        Task<string> BuscarUltimoGerenciamento ( int userId );
    }
}
