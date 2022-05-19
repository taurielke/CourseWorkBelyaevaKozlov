using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class StudentBindingModel
    {
        public int? RecordBookNumber { get; set; }
        public string StudentName { get; set; }
        public DateTime EnrollingDate { get; set; }
        public int CourseYear { get; set; }
        public int GroupId { get; set; }
        public int LearningPlanId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
