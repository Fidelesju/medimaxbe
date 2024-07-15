using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class HorarioDosagemRepository : Repository<TimeDosage>, ITimeDosageRepository
    {
        public HorarioDosagemRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(TimeDosage horarioDosagem)
        {
            DbSet.Add(horarioDosagem);
            Context.SaveChanges();
            return horarioDosagem.Id;
        }

        public void Update(TimeDosage horarioDosagem)
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
            horarioDosagem.Time = horario_dosagem;
            horarioDosagem.Treatment_Id = Treatment_id;

            // Salvando alterações
            Context.Update(horarioDosagem);
            var rowsAffected = await Context.SaveChangesAsync();

            // Retorna true se pelo menos uma linha foi afetada
            return rowsAffected > 0;
        }
    }
}
