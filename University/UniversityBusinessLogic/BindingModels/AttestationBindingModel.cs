using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class AttestationBindingModel
    { 
        public int? Id { get; set; }
        public int RecordBookNumber { get; set; }
        public int SemesterNumber { get; set; }
        public int DisciplineId { get; set; }
        public int Mark { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
