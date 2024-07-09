using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class DetalheAlimentacaoRepository : Repository<DetalheAlimentacao>, IDetalheAlimentacaoRepository
    {
        public DetalheAlimentacaoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create( DetalheAlimentacao detalheAlimentacao )
        {
            DbSet.Add(detalheAlimentacao);
            Context.SaveChanges();
            return detalheAlimentacao.id;
        }

        public void Update( DetalheAlimentacao detalheAlimentacao )
        {
            DbSet.Update(detalheAlimentacao);
            Context.SaveChanges();
        }
    }
}
