using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateUser( UserCreateRequestModel request );
        Task<int> UpdateUser ( UserUpdateRequestModel request );
        Task<int> DesactiveUser ( int id, int owner_id );
        Task<bool> UpdatePassword ( string password, int id, int owner_id );
        Task<int> ReactiveUser ( int id, int owner_id );

        Task<UserResponseModel> GetUserById(int userId);
        Task<UserResponseModel> GetUserByEmail(string email);
        Task<UserResponseModel> GetUserByName ( string name );
        Task<List<UserResponseModel>> GetUserByType ( int typeUser );
        Task<List<UserResponseModel>> GetUserByOwner ( int ownerId );
        Task<List<UserResponseModel>> GetUserByTypeAndOwnerId ( int typeUser, int ownerId );

        Task<EmailCodigoResponseModel> SendCodeToEmail ( string email, string name, int id );
    }
}
