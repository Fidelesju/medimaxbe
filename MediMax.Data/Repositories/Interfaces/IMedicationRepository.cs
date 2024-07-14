using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IMedicationRepository
    {
        int Create(Medicamentos medicine);
        void Update(Medicamentos medicine);
        Task<bool> Delete ( int medication_id, int user_id );
    }
}
