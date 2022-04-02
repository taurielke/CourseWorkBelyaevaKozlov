using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class LearningPlanViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название плана обучения")]
        public string LearningPlanName { get; set; }
        [DisplayName("Название специальности")]
        public string SpecialtyName { get; set; }
        [DisplayName("Номер семестра")]
        public int SemesterNumber { get; set; }
        public int UserId { get; set; }
        [DisplayName("ФИО пользователя")]
        public string UserName { get; set; }
        public Dictionary<int, string> DisciplineLearningPlans { get; set; } //чтобы отображались все дисциплины в плане обучения?
    }
}
