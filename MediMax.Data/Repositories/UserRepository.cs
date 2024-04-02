using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class UserRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UserRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Usuario users)
        {
            DbSet.Add(users);
            Context.SaveChanges();
            return users.id_usuario;
        }

        public void Update(Usuario users)
        {
            DbSet.Update(users);
            Context.SaveChanges();
        }
    }
}
