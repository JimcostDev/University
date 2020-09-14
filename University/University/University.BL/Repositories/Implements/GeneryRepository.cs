using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using University.BL.Data;

namespace University.BL.Repositories.Implements
{
    //implementar interfaz
    public class GeneryRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly UniversityContext universityContext; //solo se puede inicializar en el ctor
        public GeneryRepository(UniversityContext universityContext)
        {
            this.universityContext = universityContext;
        }
        public async Task<int> Count()
        {
            return await universityContext.Set<TEntity>().CountAsync();
        }
        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
                throw new Exception("The entity is null");

            universityContext.Set<TEntity>().Remove(entity);
            await universityContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await universityContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await universityContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            universityContext.Set<TEntity>().Add(entity);
            await universityContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            //universityContext.Set<TEntity>().AddOrUpdate(entity);
            universityContext.Entry(entity).State = EntityState.Modified;
            await universityContext.SaveChangesAsync();
            return entity;
        }
    }

}
