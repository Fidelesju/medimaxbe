using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<List<TreatmentResponseModel>> GetTreatmentByMedicationId(int treatmentId, int userId );
        Task<List<TreatmentResponseModel>> GetTreatmentByInterval(string startTime, string finishTime, int userId );
        Task<bool> DesactiveTreatment ( int medicineId, int treatmentId );
        Task<List<TreatmentResponseModel>> GetTreatmentActives ( int userId );
        Task<TreatmentResponseModel> GetTreatmentById ( int treatmentId, int userId );
        Task<List<TimeDosageResponseModel>> GetDosageTimeByTreatmentId ( int treatmentId );
        Task<int> CreateTreatment ( TreatmentCreateRequestModel request );
        Task<bool> UpdateMedication ( TreatmentUpdateRequestModel request );
        Task<bool> ReactiveTreatment ( int medicineId, int treatmentId );
        Task<List<TreatmentResponseModel>> GetTreatmentDesactives ( int userId );
    }
}
