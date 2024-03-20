using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class MedicamentoRepository : Repository<Medicamentos>, IMedicamentosRepository
    {
        public MedicamentoRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Medicamentos medicine)
        {
            DbSet.Add(medicine);
            Context.SaveChanges();
            return medicine.id;
        }

        public void Update(Medicamentos medicine)
        {
            DbSet.Update(medicine);
            Context.SaveChanges();
        }
    }
}
