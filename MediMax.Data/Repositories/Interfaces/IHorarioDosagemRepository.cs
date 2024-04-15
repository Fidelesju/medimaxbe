using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IHorarioDosagemRepository
    {
        int Create(HorariosDosagem horarioDosagem);
        void Update(HorariosDosagem horarioDosagem);
        Task<bool> Update(string horario_dosagem, int tratamento_id);
    }
}
