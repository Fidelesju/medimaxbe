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

        public int Create( StatusDispenser statusDispenser )
        {
            DbSet.Add(statusDispenser);
            Context.SaveChanges();
            return statusDispenser.Id;
        }

        public void Update( StatusDispenser statusDispenser )
        {
            DbSet.Update(statusDispenser);
            Context.SaveChanges();
        }
    }
}
