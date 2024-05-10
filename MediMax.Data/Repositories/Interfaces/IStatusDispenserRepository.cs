using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IStatusDispenserRepository
    {
        int Create(StatusDispenser horarioDosagem);
        void Update(StatusDispenser horarioDosagem);
    }
}
