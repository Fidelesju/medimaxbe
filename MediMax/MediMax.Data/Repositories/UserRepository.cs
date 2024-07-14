using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(User users)
        {
            DbSet.Add(users);
            Context.SaveChanges();
            return users.id_User;
        }

        public void Update(User users)
        {
            DbSet.Update(users);
            Context.SaveChanges();
        }
    }
}
