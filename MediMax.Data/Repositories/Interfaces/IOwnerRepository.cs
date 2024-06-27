using MediMax.Data.Models;
using MediMax.Data.RequestModels;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IOwnerRepository
    {
        int Create(Proprietarios owner);
        void Update(Proprietarios owner);
        //Task<bool> Update(OwnerUpdateRequestModel request);
    }
}
