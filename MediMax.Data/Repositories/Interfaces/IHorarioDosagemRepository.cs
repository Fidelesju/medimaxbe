using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IHorarioDosagemRepository
    {
        int Create(TimeDosage horarioDosagem);
        void Update(TimeDosage horarioDosagem);
        Task<bool> Update(string horario_dosagem, int Treatment_id);
    }
}
