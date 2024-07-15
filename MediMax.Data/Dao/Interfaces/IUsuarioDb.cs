using MediMax.Data.Enums;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IUserDb
    {
        Task<UserResponseModel> GetUserById(int userId);
        Task<UserResponseModel> GetUserByEmail(string name);
        Task<UserResponseModel> GetUserByName ( string name );
        Task<List<UserResponseModel>> GetUserByType ( int typeUser );
        Task<List<UserResponseModel>> GetUserByOwner ( int ownerId );
        Task<List<UserResponseModel>> GetUserByTypeAndOwnerId ( int typeUser, int ownerId );
    }
}
