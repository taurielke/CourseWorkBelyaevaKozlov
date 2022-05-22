using System;
using System.Collections.Generic;
using System.Text;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Models
{
    public class LearningPlanStudent
    {
        public int Id { get; set; }
        public string GradebookNumber { get; set; }
        public int LearningPlanId { get; set; }
        public virtual LearningPlan LearningPlan { get; set; }
        public virtual Student Student { get; set; }
    }
}
