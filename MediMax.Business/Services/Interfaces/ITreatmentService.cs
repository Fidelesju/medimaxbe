using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<List<TreatmentResponseModel>> GetTreatmentByName(string name);
        //Task<UserResponseModel> GetUserByName(string name);
    }
}
