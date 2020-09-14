using System.Collections.Generic;
using System.Threading.Tasks;
using University.BL.Models;

namespace University.BL.Services
{
    public interface ICourseService : IGenericServices<Course>
    {
        Task<bool> DeleteCheckOnEntity(int id);
        Task<IEnumerable<Student>> GetStudentByCourses(int id);
    }
}
