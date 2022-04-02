using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class LearningPlanBindingModel
    {
        public int? Id { get; set; }
        public string LearningPlanName { get; set; }
        public string SpecialtyName { get; set; }
        public int SemesterNumber { get; set; }
        public int UserId { get; set; }
        public Dictionary<int, string> DisciplineLearningPlans { get; set; }
    }
}
