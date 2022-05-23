using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class LearningPlanBindingModel
    {
        public int? Id { get; set; }
        public int? DeaneryId { get; set; }
        public string StreamName { get; set; }
        public int Hours { get; set; }
        public Dictionary<int, string> Teachers { get; set; }
        public Dictionary<int, string> Students { get; set; }
    }
}
