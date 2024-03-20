using MediMax.Data.ApplicationModels;
using MediMax.Data.RequestModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Business.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<int> CreateOwner(OwnerCreateRequestModel request);
        Task<OwnerResponseModel> GetOwnerById(int userId);
        Task<PaginatedList<OwnerResponseModel>> GetOwnerPaginatedList(Pagination pagination);
        Task<PaginatedList<OwnerResponseModel>> GetPaginatedListDesactivesOwner(Pagination pagination);
        //Task<bool> UpdateOwner(OwnerUpdateRequestModel request);

    }
}
