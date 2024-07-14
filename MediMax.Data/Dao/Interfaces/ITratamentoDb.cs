using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface ITreatmentDb
    {
        Task<List<TreatmentResponseModel>> GetTreatmentByMedicationId( int treatmentId, int userId );
        Task<List<TreatmentResponseModel>> GetTreatmentByInterval(string startTime, string finishTime, int userId );
        Task<bool> AlterandoTreatment(int remedio_id,
                string nome,
                int quantidade_medications,
                string horario_inicio,
                int intervalo_Treatment,
                int tempo_Treatment_dias,
                string recomendacoes_alimentacao,
                string observacao,
                int id);
        Task<bool> DeleteTreatment(int id);
        Task<List<TreatmentResponseModel>> GetTreatmentActives ( int userId );
        Task<TreatmentResponseModel> GetTreatmentById ( int treatmentId, int userId );
        Task<TreatmentResponseModel> BuscarTreatmentPorIdParaStatus ( int treatmentId, int userId );
        Task<List<TreatmentResponseModel>> BuscarTodosTreatmentInativos ( int userId );
    }
}
