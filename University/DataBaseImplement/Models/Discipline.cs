using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class Discipline
    {
        public int Id { get; set; }
        [Required]
        public string DisciplineName { get; set; }
        public string DisciplineDescription { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("DisciplineId")]
        public virtual List<DisciplineLearningPlan> DisciplineLearningPlans { get; set; }
    }
}
