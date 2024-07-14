using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class HorarioDosagemRepository : Repository<HorariosDosagem>, IHorarioDosagemRepository
    {
        public HorarioDosagemRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(HorariosDosagem horarioDosagem)
        {
            DbSet.Add(horarioDosagem);
            Context.SaveChanges();
            return horarioDosagem.id;
        }

        public void Update(HorariosDosagem horarioDosagem)
        {
            DbSet.Update(horarioDosagem);
            Context.SaveChanges();
        }
        public async Task<bool> Update(string horario_dosagem, int Treatment_id)
        {
            var horarioDosagem = await DbSet.FindAsync(Treatment_id);

            if (horarioDosagem == null)
            {
                return false; // Retorna false se o medicamento não for encontrado
            }

            // Atualizando os campos do medicamento
            horarioDosagem.horario_dosagem = horario_dosagem;
            horarioDosagem.tratamento_id = Treatment_id;

            // Salvando alterações
            Context.Update(horarioDosagem);
            var rowsAffected = await Context.SaveChangesAsync();

            // Retorna true se pelo menos uma linha foi afetada
            return rowsAffected > 0;
        }
    }
}
