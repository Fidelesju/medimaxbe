using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IUserDb
    {
        Task<UserResponseModel> GetUserById(int userId);
        Task<UserResponseModel> GetUserByEmail(string name);
    }
}
