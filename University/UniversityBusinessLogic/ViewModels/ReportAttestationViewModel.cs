using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class ReportAttestationViewModel
    {
        public int SemesterNumber { get; set; }
        public string DisciplineName { get; set; }
        public int Mark { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
