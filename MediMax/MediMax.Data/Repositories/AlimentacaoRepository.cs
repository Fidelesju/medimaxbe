using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class AlimentacaoRepository : Repository<Alimentacao>, IAlimentacaoRepository
    {
        public AlimentacaoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Alimentacao alimentacao)
        {
            DbSet.Add(alimentacao);
            Context.SaveChanges();
            return alimentacao.id;
        }

        public void Update(Alimentacao alimentacao)
        {
            DbSet.Update(alimentacao);
            Context.SaveChanges();
        }
    }
}
