using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IOwnerRepository
    {
        int Create(Owner owner);
        void Update(Owner owner);
        //Task<bool> Update(OwnerUpdateRequestModel request);
    }
}
