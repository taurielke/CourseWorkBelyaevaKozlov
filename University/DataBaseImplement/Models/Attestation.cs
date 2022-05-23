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
        public int SemesterNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("GradebookNumber")]
        public int StudentGradebookNumber { get; set; }
        public int DeaneryId { get; set; }
        public virtual Deanery Deanery { get; set; }

    }
}
