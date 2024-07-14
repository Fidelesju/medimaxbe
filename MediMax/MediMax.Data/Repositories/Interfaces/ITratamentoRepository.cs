using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface ITreatmentRepository
    {
        int Create(Treatment Treatments);
        void Update(Treatment Treatments);
    }
}
