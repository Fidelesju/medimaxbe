using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface ITimeDosageDb
    {
        Task<TimeDosageResponseModel> BuscarHorarioDosagemExistente(int Treatment_id, string horario_dosage);
        Task<bool> DeletandoHorarioDosagem(int Treatment_id);
        Task<List<TimeDosageResponseModel>> GetDosageTimeByTreatmentId ( int Treatment_id );
        Task<List<TimeDosageResponseModel>> GetDosageTimeByUserIdAndTime ( int userId, string time );
    }
}
