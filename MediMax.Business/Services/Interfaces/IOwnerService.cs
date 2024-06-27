using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<int> CreateOwner(OwnerCreateRequestModel request);
        Task<OwnerResponseModel> GetOwnerById(int userId);
        Task<bool> DesactiveOwner ( int ownerId );
        Task<bool> ReactiveOwner ( int ownerId );
        Task<int> UpateOwner ( OwnerUpdateRequestModel request );
    }
}
