using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        int Create(User user);
        void Update(User user);
    }
}
