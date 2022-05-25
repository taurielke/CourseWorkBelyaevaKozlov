using System;
using UniversityBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<LearningPlanViewModel> LearningPlans { get; set; }
        public List<AttestationViewModel> Attestations { get; set; }
        public int DeaneryId { get; set; }
    }
}
