using MediMax.Data.Models;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        int Create(User user);
        void Update(User user);
        Task<bool> Update ( UserResponseModel request );
        Task<bool> Reactive ( int id, int owner_id );
        Task<bool> Desactive ( int id, int owner_id );
        Task<bool> UpdatePassword ( string password, int id, int owner_id );
    }
}
