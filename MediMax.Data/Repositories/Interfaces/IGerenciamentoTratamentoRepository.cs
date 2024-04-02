using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IGerenciamentoTratamentoRepository
    {
        int Create(GerenciamentoTratamento gerenciamentoTratamentos);
        void Update(GerenciamentoTratamento gerenciamentoTratamentos);
    }
}
