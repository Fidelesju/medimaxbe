using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface ITreatmentManagementDbDb
    {
        Task<List<TreatmentManagementResponseModel>> GetAllHistoric( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoDataEspecifica(string data, int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoUltimoAno( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistorico60Dias( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistorico30Dias(int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistorico15Dias( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistorico7Dias( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoNaoTomado( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoTomado( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoPorMedicamento(string nome, int userId );
        Task<TreatmentManagementResponseModel> BuscarStatusDoUltimoGerenciamento (int userId );
        Task<TreatmentManagementResponseModel> BuscarUltimoGerenciamento ( int userId );
        Task<List<TreatmentManagementResponseModel>> BuscarHistoricoAnoEspecifico ( string year, int userId );
    }
}
