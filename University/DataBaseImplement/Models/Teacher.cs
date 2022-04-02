using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        public string TeacherName { get; set; }
    }
}
