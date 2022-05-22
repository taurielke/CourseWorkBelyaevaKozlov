using System;
using System.Collections.Generic;
using System.Text;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Models
{
    public class LearningPlanTeacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int LearningPlanId { get; set; }
        public virtual LearningPlan LearningPlan { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
