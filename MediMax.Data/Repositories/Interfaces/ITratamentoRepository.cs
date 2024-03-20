using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface ITratamentoRepository
    {
        int Create(Tratamento tratamentos);
        void Update(Tratamento tratamentos);
    }
}
