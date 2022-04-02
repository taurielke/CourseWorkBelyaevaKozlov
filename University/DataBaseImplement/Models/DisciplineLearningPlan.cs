using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class DisciplineLearningPlan
    {
        public int Id { get; set; }
        public int DisciplineId { get; set; }
        public int LearningPlanId { get; set; }

        public virtual Discipline Discipline { get; set; }
        public virtual LearningPlan LearningPlan { get; set; }
    }
}
