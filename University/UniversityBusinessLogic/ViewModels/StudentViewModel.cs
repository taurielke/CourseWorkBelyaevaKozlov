using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class StudentViewModel
    {
        public int RecordBookNumber { get; set; }
        [DisplayName("ФИО студента")]
        public string StudentName { get; set; }
        [DisplayName("Дата поступления")]
        public DateTime EnrollingDate { get; set; }
        [DisplayName("Курс")]
        public int CourseYear { get; set; }
        public int GroupId { get; set; }
        [DisplayName("Название группы")]
        public string GroupName { get; set; }
        public Dictionary<int, string> StudentLearningPlans { get; set; }

    }
}
