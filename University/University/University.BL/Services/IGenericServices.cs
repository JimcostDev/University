using System.Collections.Generic;
using System.Threading.Tasks;

namespace University.BL.Services
{
    public interface IGenericServices<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(int id);
    }
}
