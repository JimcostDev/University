﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.BL.Models
{
    [Table("Department", Schema = "dbo")]
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public float Budget { get; set; }
        public DateTime StartDate { get; set; }
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }

        //tablas referenciadas (modelos)
        public Instructor Instructor { get; set; }
    }
}
