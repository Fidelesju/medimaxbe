using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IOwnerAuthDb
    {
        Task<LoginOwnerResponseModel> AuthenticateUserOwner(string email, string password);
    }
}
