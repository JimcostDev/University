using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class StudentDTO
    {
        public int ID { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The LastName is required")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The First Name is required")]
        [StringLength(50)]
        public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", LastName, FirstMidName);
            }
        }
        public ICollection<EnrollmentDTO> Enrollment { get; set; }
    }
}
