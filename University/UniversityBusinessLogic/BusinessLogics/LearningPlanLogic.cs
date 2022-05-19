using System;
using System.Collections.Generic;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class LearningPlanLogic : ILearningPlanLogic
    {
        private readonly ILearningPlanStorage _learningPlanStorage;

        public LearningPlanLogic(ILearningPlanStorage learningPlanStorage)
        {
            _learningPlanStorage = learningPlanStorage;
        }

        public List<LearningPlanViewModel> Read(LearningPlanBindingModel model)
        {
            if (model == null)
            {
                return _learningPlanStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<LearningPlanViewModel>
                {
                    _learningPlanStorage.GetElement(model)
                };
            }
            return _learningPlanStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(LearningPlanBindingModel model)
        {
            var element = _learningPlanStorage.GetElement(new LearningPlanBindingModel
            {
                LearningPlanName = model.LearningPlanName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Такой учебный план уже существует!");
            }
            if (model.Id.HasValue)
            {
                _learningPlanStorage.Update(model);
            }
            else
            {
                _learningPlanStorage.Insert(model);
            }
        }

        public void Delete(LearningPlanBindingModel model)
        {
            var element = _learningPlanStorage.GetElement(new LearningPlanBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Учебный план не найден");
            }
            _learningPlanStorage.Delete(model);
        }
    }
}
