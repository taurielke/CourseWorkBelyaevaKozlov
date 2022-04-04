using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class LearningPlan
    {
        public int Id { get; set;}
        [Required]
        public string LearningPlanName { get; set; }
        public string SpecialtyName { get; set; }
        public int UserId { get; set; }
        public int RecordBookNumber { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("LearningPlanId")]
        public virtual List<DisciplineLearningPlan> DisciplineLearningPlans { get; set; }
        [ForeignKey("LearningPlanId")]
        public virtual List<Student> Students { get; set; }
    }
}
