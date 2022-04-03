using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class InterimReportViewModel
    {
        public int? Id { get; set; }

        public int RecordBookNumber { get; set; }
        [DisplayName("Номер семестра")]
        public int SemesterNumber { get; set; }
        [DisplayName("ФИО студента")]
        public string StudentName { get; set; }
        public int DisciplineId { get; set; }
        [DisplayName("Название дисциплины")]
        public string DisciplineName { get; set; }
        [DisplayName("Оценка")]
        public int Mark { get; set; }
    }
}
