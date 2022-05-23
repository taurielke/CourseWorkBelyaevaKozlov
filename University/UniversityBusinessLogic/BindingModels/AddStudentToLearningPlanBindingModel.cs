using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BindingModels
{
    public class AddStudentToLearningPlanBindingModel
    {
        public int GradebookNumber { get; set; }
        public List<int> LearningPlansId { get; set; }
    }
}
