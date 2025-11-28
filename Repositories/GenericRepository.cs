//Repositorio general para todas las entidades
//Repositories are used to interact with the database, taking just the necessary data instead of the whole entity
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyStore.Context;

namespace MyStore.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) where TEntity : class
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync() //This works as a generic method to get all entities of any type
        {
            return await _dbContext.Set<TEntity>().ToListAsync(); // The returned value is awaited asynchronously. 
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>[]? conditions = null, //[] defines the array bc there are many products
            Expression<Func<TEntity, object>>[]? includes = null
            )
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

        if (conditions is not null)
            foreach (var condition in conditions) query = query.Where(condition);

         if (includes is not null)
            foreach (var include in includes) query = query.Include(include); //We use foreach bc it can receive diff data.

            return await query.ToListAsync(); //returns the requested data linked(anidado) with another entity. 
        }

        //Async means that the method runs in the background, allowing other operations to continue without waiting for it to finish.
        public virtual async Task AddAsync(TEntity entity) // Generic method to add an entity of any type
        // Virtual allows derived classes to override this method if needed
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync(); // Save changes to the database
        }

        public async Task<TEntity?> GetByIdAsync(int entityId) // Generic method to get an entity by its ID
        {
            return await _dbContext.Set<TEntity>().FindAsync(entityId);
        }

        public async Task EditAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        //Basic CRUD operations are covered in this generic repository
    }
}


