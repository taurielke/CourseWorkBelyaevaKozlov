using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class ReportDisciplinesInLearningPlansViewModel
    {
        public string LearningPlanName { get; set; }
        public List<DisciplineViewModel> Disciplines { get; set; }
    }
}
