using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.BL.Models
{
    [Table("OfficeAssignment", Schema = "dbo")]
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        public string Location { get; set; }

        //dependencias (modelos):
        public Instructor Instructor { get; set; }
    }
}
