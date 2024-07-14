using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class DispenserStatusRepository : Repository<DispenserStatus>, IDispenserStatusRepository
    {
        public DispenserStatusRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(DispenserStatus horarioDosagem)
        {
            DbSet.Add(horarioDosagem);
            Context.SaveChanges();
            return horarioDosagem.id;
        }

        public void Update(DispenserStatus horarioDosagem)
        {
            DbSet.Update(horarioDosagem);
            Context.SaveChanges();
        }
    }
}
