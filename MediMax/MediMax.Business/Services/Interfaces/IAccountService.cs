using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponseModel> AuthenticateUser(LoginRequestModel loginRequest);
        Task<LoginAdminResponseModel> AuthenticateUserAdmin(LoginRequestModel loginRequest);
        Task<LoginOwnerResponseModel> AuthenticateUserOwner(LoginRequestModel loginRequest);
    }
}
