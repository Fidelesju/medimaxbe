using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IMedicamentoDb
    {
        Task<List<MedicamentoResponseModel>> BuscarTodosMedicamentos();
        Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorNome(string name);
        Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorDataVencimento();
        Task<MedicamentoResponseModel> BuscarMedicamentosPorTratamento(int tratamentoId);
        Task<bool> DeletandoMedicamento(int id);
        Task<bool> AlterandoMedicamento(string nome, string data_vencimento, int quantidade_embalagem, float dosagem, int id);
        Task<List<MedicamentoResponseModel>> BuscarMedicamentosInativos ( );
    }
}
