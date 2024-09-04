using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IMedicationService
    {
        Task<BaseResponse<int>> CreateMedication ( MedicationCreateRequestModel request );
        Task<List<MedicationResponseModel>> GetAllMedicine( int userId );
        Task<bool> UpdateMedication( MedicationUpdateRequestModel request );
        Task<List<MedicationResponseModel>> GetMedicationByName(string name, int userId );
        Task<List<MedicationResponseModel>> GetMedicationByExpirationDate( int userId );
        Task<bool> DeactivateMedication ( int medicineId, int userId );
        Task<bool> ReactiveMedication ( int medicineId, int userId );
        Task<MedicationResponseModel> GetMedicationByTreatmentId ( int TreatmentId , int userId );
        Task<MedicationResponseModel> GetMedicationById ( int medicationId, int userId );
    }
}
