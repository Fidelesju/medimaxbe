using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class GerenciamentoTratamentoRepository : Repository<GerenciamentoTratamento>, IGerenciamentoTratamentoRepository
    {
        public GerenciamentoTratamentoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(GerenciamentoTratamento gerenciamentoTratamentos)
        {
            if (gerenciamentoTratamentos == null)
            {
                throw new ArgumentNullException(nameof(gerenciamentoTratamentos));
            }

            DbSet.Add(gerenciamentoTratamentos);
            Context.SaveChanges();
            return gerenciamentoTratamentos.id;
        }

        public void Update(GerenciamentoTratamento gerenciamentoTratamentos)
        {
            if (gerenciamentoTratamentos == null)
            {
                throw new ArgumentNullException(nameof(gerenciamentoTratamentos));
            }

            DbSet.Update(gerenciamentoTratamentos);
            Context.SaveChanges();
        }
    }
}
