using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IHistoricoDb
    {
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
    }
}
