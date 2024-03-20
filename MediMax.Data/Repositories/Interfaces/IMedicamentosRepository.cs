using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IMedicamentosRepository
    {
        int Create(Medicamentos medicine);
        void Update(Medicamentos medicine);
    }
}
