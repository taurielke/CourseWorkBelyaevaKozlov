using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class DocReportWarehouserBindingModel
    {
        public string FileName { get; set; }
        public int? DisciplineId { get; set; }
        public int? TeacherId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
