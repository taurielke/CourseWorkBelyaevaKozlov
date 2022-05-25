using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class ReportAttestationsViewModel
    {
        public string StudentName { get; set; }
        public DateTime AttestationDate { get; set; }
        public List<(LearningPlanViewModel, List<DisciplineViewModel>)> LearningPlanDisciplines { get; set; }
    }
}
