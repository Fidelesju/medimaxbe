using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface ITreatmentRepository
    {
        int Create(Treatment Treatments);
        void Update(Treatment Treatments);
        Task<bool> Delete ( int medication_id, int treatment_id );

    }
}
