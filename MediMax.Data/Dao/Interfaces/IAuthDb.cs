using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IAuthDb 
    {
        Task<LoginResponseModel> AuthenticateUser(string email, string login,string password);
    }
}
