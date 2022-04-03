using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class StudentLearningPlan
    {
        public int Id { get; set; }
        public int LearningPlanId { get; set; }
        public int RecordBookNumber { get; set; }
        public virtual LearningPlan LearningPlan { get; set; }
        public virtual Student Student { get; set; }
    }
}
