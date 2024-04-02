using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IMedicamentoService
    {
        Task<int> CriandoMedicamentosETratamento(MedicamentoETratamentoCreateRequestModel request);
        Task<List<MedicamentoResponseModel>> BuscarTodosMedicamentos();
        //Task<UserResponseModel> GetUserByName(string name);
    }
}
