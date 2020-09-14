using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class InstructorRepository : GeneryRepository<Instructor>, IInstructorRepository
    {
        private readonly UniversityContext universityContext;
        public InstructorRepository(UniversityContext universityContext) : base(universityContext)
        {
            this.universityContext = universityContext;
        }

        public async Task<IEnumerable<Course>> GetCoursesByInstructor(int id)
        {

            //SELECT course.*
            //  FROM[University].[dbo].[CourseInstructor] courseInst
            //  JOIN[dbo].[Course] course ON course.CourseID = courseInst.CourseID --RELACION DE LA FK A LA PK
            //  WHERE courseInst.InstructorID = 5
            var courses = universityContext.CourseInstructors
                            .Include("Course")
                            .Where(x => x.InstructorID == id)
                            .Select(x => x.Course);

            ////*****************OTRA FORMA DE HACER LA CONSULTA*****************
            //var _courses = (from courseInstructors in universityContext.CourseInstructors
            //                join course in universityContext.Courses on courseInstructors.CourseID
            //                equals course.CourseID
            //                where courseInstructors.InstructorID == id
            //                select course);
            return await courses.ToListAsync();
        }
    }
}
