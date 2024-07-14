using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface ITreatmentManagementRepository
    {
        int Create(TreatmentManagement TreatmentManagements);
        void Update(TreatmentManagement TreatmentManagements);
    }
}
