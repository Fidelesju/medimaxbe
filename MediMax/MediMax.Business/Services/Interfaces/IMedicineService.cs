using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IMedicineService
    {
        Task<int> CreateMedicineAndTreatment(MedicamentoETreatmentCreateRequestModel request);
        Task<List<MedicineResponseModel>> GetAllMedicine();
        //Task<UserResponseModel> GetUserByName(string name);
    }
}
