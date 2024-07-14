using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IHorariosDosagemDb
    {
        Task<HorariosDosagemResponseModel> BuscarHorarioDosagemExistente(int Treatment_id, string horario_dosage);
        Task<bool> DeletandoHorarioDosagem(int Treatment_id);
        Task<List<HorariosDosagemResponseModel>> GetDosageTimeByTreatmentId ( int Treatment_id );
    }
}
