using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IOwnerDb
    {
        Task<OwnerResponseModel> GetOwnerById(int ownerId);
        Task<bool> DesactiveOwner ( int ownerId );
        Task<bool> ReactiveOwner ( int ownerId );
        Task<OwnerResponseModel> UpdateOwner ( OwnerUpdateRequestModel request );
    }
}
