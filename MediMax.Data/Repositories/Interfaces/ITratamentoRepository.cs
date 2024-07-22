using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface ITreatmentRepository
    {
        int Create(Treatment Treatments);
        void Update(Treatment Treatments);
        Task<bool> Desactive ( int user_id, int treatment_id );
        Task<bool> Update ( TreatmentResponseModel request );
        Task<bool> Reactive ( int user_id, int treatment_id );
    }
}
