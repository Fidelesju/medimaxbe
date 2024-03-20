using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateUsers(UserCreateRequestModel request);
        Task<UserResponseModel> GetUserById(int userId);
        Task<UserResponseModel> GetUserByEmail(string name);
    }
}
