using Microsoft.EntityFrameworkCore;
using MediMax.Data.Models;
using MediMax.Data.Repositories.Interfaces;

namespace MediMax.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MediMaxDbContext Context;
        protected readonly DbSet<T> DbSet;

        public Repository(MediMaxDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public async Task CreateAsync(T entidade)
        {
            await DbSet.AddAsync(entidade);
            await SaveChangesAsync();
        }

        public async Task CreateAsync(List<T> entidadeLista)
        {
            await DbSet.AddRangeAsync(entidadeLista);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entidade)
        {
            DbSet.Update(entidade);
            await SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        protected async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();
                throw;
            }
        }
    }
}