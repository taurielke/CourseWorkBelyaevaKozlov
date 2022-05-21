using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class Deanery
    {
        public int Id { get; set; }

        [Required]
        public string DeaneryName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("DeaneryId")]
        public virtual List<LearningPlan> LearningPlans { get; set; }
    }
}
