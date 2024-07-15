using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class DetalheAlimentacaoRepository : Repository<NutritionDetail>, IDetalheAlimentacaoRepository
    {
        public DetalheAlimentacaoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create( NutritionDetail detalheAlimentacao )
        {
            DbSet.Add(detalheAlimentacao);
            Context.SaveChanges();
            return detalheAlimentacao.Id;
        }

        public void Update( NutritionDetail detalheAlimentacao )
        {
            DbSet.Update(detalheAlimentacao);
            Context.SaveChanges();
        }
    }
}
