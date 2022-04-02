using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface ILearningPlanStorage
    {
        List<LearningPlanViewModel> GetFullList();
        List<LearningPlanViewModel> GetFilteredList(LearningPlanBindingModel model);
        LearningPlanViewModel GetElement(LearningPlanBindingModel model);
        void Insert(LearningPlanBindingModel model);
        void Update(LearningPlanBindingModel model);
        void Delete(LearningPlanBindingModel model);
    }
}
