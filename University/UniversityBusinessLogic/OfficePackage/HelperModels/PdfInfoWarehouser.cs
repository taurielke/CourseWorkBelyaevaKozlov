using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoWarehouser
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string TeacherName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportInterimReportViewModel> InterimReports { get; set; }
        public string DisciplineName { get; internal set; }
    }
}
