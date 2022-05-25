using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class InterimReportLogic : IInterimReportLogic
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
                return new List<InterimReportViewModel> { _interimReportStorage.GetElement(model) };
            }
            return _interimReportStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(InterimReportBindingModel model)
        {
            var element = _interimReportStorage.GetElement(new InterimReportBindingModel
            {
                DateOfExam = model.DateOfExam,
                TeacherId = model.TeacherId,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть ведомость с этим преподавателем и той же датой");
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
            var element = _interimReportStorage.GetElement(new InterimReportBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _interimReportStorage.Delete(model);
        }
    }
}
