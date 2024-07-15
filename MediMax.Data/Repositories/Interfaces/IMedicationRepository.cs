using MediMax.Data.Models;
using MediMax.Data.ResponseModels;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IMedicationRepository
    {
        int Create(Medication medicine);
        void Update(Medication medicine);
        Task<bool> Update ( MedicationResponseModel request );
        Task<bool> Delete ( int medication_id, int user_id );
        Task<bool> Reactive ( int medication_id, int user_id );
        Task<bool> Desactive ( int medication_id, int user_id );
    }
}
