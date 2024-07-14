using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IMedicationRepository
    {
        int Create(Medication medicine);
        void Update(Medication medicine);
        Task<bool> Delete ( int medication_id, int user_id );
    }
}
