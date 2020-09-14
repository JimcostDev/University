using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.BL.Repositories;

namespace University.BL.Services.Implements
{
    public class GenericService<TEntity> : IGenericServices<TEntity> where TEntity : class
    {
        private IGenericRepository<TEntity> _genericRepository;
        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
    }

        public async Task Delete(int id)
        {
            await _genericRepository.Delete(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
           return await _genericRepository.GetAll();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _genericRepository.GetById(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            return await _genericRepository.Insert(entity);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            return await _genericRepository.Update(entity);
        }
    }
}
