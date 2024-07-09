using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IDetalheAlimentacaoRepository
    {
        int Create(DetalheAlimentacao detalheAlimentacao);
        void Update( DetalheAlimentacao detalheAlimentacao );
    }
}
