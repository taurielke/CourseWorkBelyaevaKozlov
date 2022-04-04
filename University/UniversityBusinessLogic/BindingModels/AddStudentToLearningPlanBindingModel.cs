using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BindingModels
{
    public class AddStudentToLearningPlanBindingModel
    {
        public int LearningPlanId { get; set; }
        public StudentViewModel Student { get; set; }
    }
}
