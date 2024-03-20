using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IMedicineService
    {
        Task<int> CreateMedicineAndTreatment(MedicamentoETratamentoCreateRequestModel request);
        Task<List<MedicineResponseModel>> GetAllMedicine();
        //Task<UserResponseModel> GetUserByName(string name);
    }
}
