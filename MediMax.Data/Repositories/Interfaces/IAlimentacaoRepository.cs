using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IAlimentacaoRepository
    {
        int Create(Alimentacao food);
        void Update(Alimentacao food);
    }
}
