using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class TratamentoRepository : Repository<Tratamento>, ITratamentoRepository
    {
        public TratamentoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Tratamento tratamentos)
        {
            DbSet.Add(tratamentos);
            Context.SaveChanges();
            return tratamentos.id;
        }

        public void Update(Tratamento tratamentos)
        {
            DbSet.Update(tratamentos);
            Context.SaveChanges();
        }
    }
}
