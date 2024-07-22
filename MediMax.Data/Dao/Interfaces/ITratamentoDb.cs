using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface ITreatmentDb
    {
        Task<List<TreatmentResponseModel>> GetTreatmentByMedicationId( int treatmentId, int userId );
        Task<List<TreatmentResponseModel>> GetTreatmentByInterval(string startTime, string finishTime, int userId );
        
        Task<List<TreatmentResponseModel>> GetTreatmentActives ( int userId );
        Task<TreatmentResponseModel> GetTreatmentById ( int treatmentId, int userId );
        Task<TreatmentResponseModel> BuscarTreatmentPorIdParaStatus ( int treatmentId, int userId );
        Task<List<TreatmentResponseModel>> GetTreatmentInactives ( int userId );
        Task<List<TreatmentResponseModel>> GetTreatmentByMedicationName ( int medicineId, int userId, string name );
        Task<List<TreatmentResponseModel>> GetTreatmentDesactives ( int userId );
        }
}
