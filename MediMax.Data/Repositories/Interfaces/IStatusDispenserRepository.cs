using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IDispenserStatusRepository
    {
        int Create(DispenserStatus horarioDosagem);
        void Update(DispenserStatus horarioDosagem);
    }
}
