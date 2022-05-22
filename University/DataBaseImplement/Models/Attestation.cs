using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class Attestation
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("GradebookNumber")]
        public string StudentGradebookNumber { get; set; }
        [ForeignKey("DepartmentLogin")]
        public string DeaneryLogin { get; set; }
    }
}
