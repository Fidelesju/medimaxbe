using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IDispenserStatusRepository
    {
        int Create( StatusDispenser horarioDosagem);
        void Update( StatusDispenser horarioDosagem );
    }
}
