using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IMedicamentoService
    {
        Task<int> CriandoMedicamentosETratamento(MedicamentoETratamentoCreateRequestModel request);
        Task<List<MedicamentoResponseModel>> BuscarTodosMedicamentos( int userId );
        Task<bool> AlterandoMedicamentosETratamento(MedicamentoETratamentoUpdateRequestModel request);
        Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorNome(string name, int userId );
        Task<List<MedicamentoResponseModel>> BuscarMedicamentosPorDataVencimento( int userId );
        Task<bool> DeletandoMedicamento(int medicineId, int tratamentoId, int userId );
        Task<MedicamentoResponseModel> BuscarMedicamentosPorTratamento ( int tratamentoId , int userId );
    }
}
