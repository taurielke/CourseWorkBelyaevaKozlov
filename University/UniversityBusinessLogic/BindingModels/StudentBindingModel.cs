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
        public Dictionary<int, string> StudentLearningPlans { get; set; }
    }
}
