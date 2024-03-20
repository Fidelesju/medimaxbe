using MediMax.Data.ApplicationModels;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Dao.Interfaces
{
    public interface IOwnerDb
    {
        Task<OwnerResponseModel> GetOwnerById(int ownerId);
        Task<PaginatedList<OwnerResponseModel>> GetPaginatedListOwners(Pagination pagination);
        Task<PaginatedList<OwnerResponseModel>> GetPaginatedListDesactivesOwner(Pagination pagination);
    }
}
