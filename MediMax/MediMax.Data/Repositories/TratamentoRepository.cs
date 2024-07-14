using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class TreatmentRepository : Repository<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Treatment Treatments)
        {
            DbSet.Add(Treatments);
            Context.SaveChanges();
            return Treatments.id;
        }

        public void Update(Treatment Treatments)
        {
            DbSet.Update(Treatments);
            Context.SaveChanges();
        }
    }
}
