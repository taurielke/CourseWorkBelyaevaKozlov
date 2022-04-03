using System;
using System.Collections.Generic;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class DisciplineLogic
    {
        private readonly IDisciplineStorage _disciplineStorage;

        public DisciplineLogic(IDisciplineStorage disciplineStorage)
        {
            _disciplineStorage = disciplineStorage;
        }

        public List<DisciplineViewModel> Read(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return _disciplineStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DisciplineViewModel>
                {
                    _disciplineStorage.GetElement(model)
                };
            }
            return _disciplineStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DisciplineBindingModel model)
        {
            var element = _disciplineStorage.GetElement(new DisciplineBindingModel
            {
                DisciplineName = model.DisciplineName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Такая дисциплина уже существует!");
            }
            if (model.Id.HasValue)
            {
                _disciplineStorage.Update(model);
            }
            else
            {
                _disciplineStorage.Insert(model);
            }
        }

        public void Delete(DisciplineBindingModel model)
        {
            var element = _disciplineStorage.GetElement(new DisciplineBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Дисциплина не найдена");
            }
            _disciplineStorage.Delete(model);
        }
    }
}
