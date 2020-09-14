using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BL.DTOs
{
    public class EnrollmentDTO
    {

        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }
        public char Grade { get; set; }

        //dependencias (modelos):
        public CourseDTO Course { get; set; }
        public StudentDTO Student { get; set; }
    }
}
