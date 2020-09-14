using System.Collections.Generic;
using System.Threading.Tasks;

namespace University.BL.Repositories
{
    //Interface : define las reglas que las entidades deben cumplir al momento de implementarlas (contrato)
    //me dice que hacer, mas no como implementarlo
    public interface IGenericRepository<TEntity> where TEntity : class //recibe un objeto donde sea una clase
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(int id);
    }
}
