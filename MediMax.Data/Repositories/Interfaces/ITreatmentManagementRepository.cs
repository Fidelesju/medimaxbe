using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface ITreatmentManagementRepository
    {
        int Create(TreatmentManagement treatmentManagement);
        void Update(TreatmentManagement treatmentManagement);
    }
}
