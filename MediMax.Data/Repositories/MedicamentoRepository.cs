using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class MedicamentoRepository : Repository<Medicamentos>, IMedicamentosRepository
    {
        public MedicamentoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Medicamentos medicine)
        {
            DbSet.Add(medicine);
            Context.SaveChanges();
            return medicine.id;
        }

        public void Update(Medicamentos medicine)
        {
            DbSet.Update(medicine);
            Context.SaveChanges();
        }

        public async Task<bool> Update(string nome, string data_vencimento, int quantidade_embalagem, float dosagem, int id)
        {
            var medicine = await DbSet.FindAsync(id);
            
            if (medicine == null)
            {
                return false; // Retorna false se o medicamento não for encontrado
            }
            
            // Atualizando os campos do medicamento
            medicine.nome = nome;
            medicine.data_vencimento = data_vencimento;
            medicine.quantidade_embalagem = quantidade_embalagem;
            medicine.dosagem = dosagem;

            // Salvando alterações
            Context.Update(medicine);
            var rowsAffected = await Context.SaveChangesAsync();
            
            // Retorna true se pelo menos uma linha foi afetada
            return rowsAffected > 0;
        }
    }
}
