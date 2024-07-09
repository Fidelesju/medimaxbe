using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IHistoricoDb
    {
        Task<List<HistoricoResponseModel>> BuscarHistoricoGeral( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoDataEspecifica(string data, int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoUltimoAno( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistorico60Dias( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistorico30Dias(int userId );
        Task<List<HistoricoResponseModel>> BuscarHistorico15Dias( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistorico7Dias( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoNaoTomado( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoTomado( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoPorMedicamento(string nome, int userId );
        Task<HistoricoResponseModel> BuscarStatusDoUltimoGerenciamento (int userId );
        Task<HistoricoResponseModel> BuscarUltimoGerenciamento ( int userId );
        Task<List<HistoricoResponseModel>> BuscarHistoricoAnoEspecifico ( string year, int userId );
    }
}
