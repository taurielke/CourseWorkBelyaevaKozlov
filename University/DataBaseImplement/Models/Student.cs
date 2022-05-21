using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class Student
    {
        [Key]
        public int RecordBookNumber { get; set; }
        [Required]
        public string StudentName { get; set; }
        public DateTime EnrollingDate { get; set; }
        [Required]
        public int CourseYear { get; set; }
        public int LearningPlanId { get; set; }
        public virtual LearningPlan LearningPlan { get; set; }
    }
}
