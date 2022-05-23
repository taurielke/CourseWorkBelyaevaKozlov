using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class AttestationBindingModel
    {
        public int? Id { get; set; }
        public int? DeaneryId { get; set; }
        public int SemesterNumber { get; set; }
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
