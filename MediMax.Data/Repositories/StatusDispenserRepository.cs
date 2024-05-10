using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class StatusDispenserRepository : Repository<StatusDispenser>, IStatusDispenserRepository
    {
        public StatusDispenserRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(StatusDispenser horarioDosagem)
        {
            DbSet.Add(horarioDosagem);
            Context.SaveChanges();
            return horarioDosagem.id;
        }

        public void Update(StatusDispenser horarioDosagem)
        {
            DbSet.Update(horarioDosagem);
            Context.SaveChanges();
        }
    }
}
