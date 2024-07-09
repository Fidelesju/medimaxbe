using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IMedicamentoDb
    {
        Task<List<MedicamentoResponseModel>> BuscarTodosMedicamentos(int userId);
        Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorNome(string name, int userId);
        Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorDataVencimento( int userId );
        Task<MedicamentoResponseModel> BuscarMedicamentosPorTratamento(int tratamentoId, int userId );
        Task<bool> DeletandoMedicamento(int id, int userId );
        Task<bool> AlterandoMedicamento(string nome, string data_vencimento, int quantidade_embalagem, float dosagem, int id, int userId );
        Task<List<MedicamentoResponseModel>> BuscarMedicamentosInativos ( int userId );
    }
}
