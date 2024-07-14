using MediMax.Data.Enums;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IUserDb
    {
        Task<UserResponseModel> GetUserById(int userId);
        Task<UserResponseModel> GetUserByEmail(string name);
        Task<UserResponseModel> GetUserByName ( string name );
        Task<List<UserResponseModel>> GetUserByTypeUser ( int typeUser );
        Task<List<UserResponseModel>> GetUserByOwner ( int ownerId );
        Task<List<UserResponseModel>> GetUserByOwnerOfTypeUser ( int typeUser, int ownerId );
        Task<int> UpdateUser ( UserUpdateRequestModel request );
        Task<int> DesativarUser ( int userId );
        Task<bool> AlterarSenha ( int userId, string password );
        Task<int> ReativarUser ( int userId );
    }
}
