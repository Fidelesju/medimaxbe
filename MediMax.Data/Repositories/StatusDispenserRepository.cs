using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class DispenserStatusRepository : Repository<StatusDispenser>, IDispenserStatusRepository
    {
        public DispenserStatusRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create( StatusDispenser horarioDosagem )
        {
            DbSet.Add(horarioDosagem);
            Context.SaveChanges();
            return horarioDosagem.Id;
        }

        public void Update( StatusDispenser horarioDosagem )
        {
            DbSet.Update(horarioDosagem);
            Context.SaveChanges();
        }
    }
}
