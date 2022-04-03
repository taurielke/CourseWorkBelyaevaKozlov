using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class DisciplineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название дисциплины")]
        public string DisciplineName { get; set; }
        [DisplayName("Описание дисциплины")]
        public string DisciplineDescription { get; set; }
        public int TeacherId { get; set; }
        public Dictionary<int, string> DisciplineLearningPlans { get; set; }
    }
}
