using System;
using System.Collections.Generic;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class InterimReportLogic
    {
        private readonly IInterimReportStorage _interimReportStorage;

        public InterimReportLogic(IInterimReportStorage interimReportStorage)
        {
            _interimReportStorage = interimReportStorage;
        }

        public List<InterimReportViewModel> Read(InterimReportBindingModel model)
        {
            if (model == null)
            {
                return _interimReportStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<InterimReportViewModel>
                {
                    _interimReportStorage.GetElement(model)
                };
            }
            return _interimReportStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(InterimReportBindingModel model)
        {
            var element = _interimReportStorage.GetElement(new InterimReportBindingModel
            {
                RecordBookNumber = model.RecordBookNumber,
                SemesterNumber = model.SemesterNumber,
                DisciplineId = model.DisciplineId
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Промежуточная ведомость по этой дисциплине уже занесена!");
            }
            if (model.Id.HasValue)
            {
                _interimReportStorage.Update(model);
            }
            else
            {
                _interimReportStorage.Insert(model);
            }
        }

        public void Delete(InterimReportBindingModel model)
        {
            var element = _interimReportStorage.GetElement(new InterimReportBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Промежуточная ведомость не найдена");
            }
            _interimReportStorage.Delete(model);
        }
    }
}
