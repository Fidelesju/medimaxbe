using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IAdminAuthDb
    {
        Task<LoginAdminResponseModel> AuthenticateUserAdmin(string email, string password);
    }
}
