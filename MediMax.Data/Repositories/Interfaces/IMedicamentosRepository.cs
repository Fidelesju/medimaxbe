using MediMax.Data.Models;

namespace MediMax.Data.Repositories.Interfaces
{
    public interface IMedicamentosRepository
    {
        int Create(Medicamentos medicine);
        void Update(Medicamentos medicine);
        Task<bool> Update(string nome, string data_vencimento, int quantidade_embalagem, float dosagem, int id);
    }
}
