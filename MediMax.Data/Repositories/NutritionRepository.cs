using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public class NutritionRepository : Repository<Nutrition>, INutritionRepository
    {
        public NutritionRepository(MediMaxDbContext context) : base(context)
        {
        }

        public int Create(Nutrition alimentacao)
        {
            DbSet.Add(alimentacao);
            Context.SaveChanges();
            return alimentacao.Id;
        }

        public void Update(Nutrition alimentacao)
        {
            DbSet.Update(alimentacao);
            Context.SaveChanges();
        }
    }
}
