﻿using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface ILearningPlanLogic
    {
        List<LearningPlanViewModel> Read(LearningPlanBindingModel model);
        void CreateOrUpdate(LearningPlanBindingModel model);
        void Delete(LearningPlanBindingModel model);
    }
}
