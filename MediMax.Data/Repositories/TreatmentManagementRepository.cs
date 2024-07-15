using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class TreatmentManagementRepository : Repository<TreatmentManagement>, ITreatmentManagementRepository
    {
        public TreatmentManagementRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(TreatmentManagement TreatmentManagements)
        {
            if (TreatmentManagements == null)
            {
                throw new ArgumentNullException(nameof(TreatmentManagements));
            }

            DbSet.Add(TreatmentManagements);
            Context.SaveChanges();
            return TreatmentManagements.Id;
        }

        public void Update(TreatmentManagement TreatmentManagements)
        {
            if (TreatmentManagements == null)
            {
                throw new ArgumentNullException(nameof(TreatmentManagements));
            }

            DbSet.Update(TreatmentManagements);
            Context.SaveChanges();
        }
    }
}
