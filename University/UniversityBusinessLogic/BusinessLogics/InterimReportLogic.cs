using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class InterimReportLogic : IInterimReportLogic
    {
        private readonly IInterimReportStorage _checkListStorage;
        public InterimReportLogic(IInterimReportStorage checkListStorage)
        {
            _checkListStorage = checkListStorage;
        }
        public List<InterimReportViewModel> Read(InterimReportBindingModel model)
        {
            if (model == null)
            {
                return _checkListStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<InterimReportViewModel> { _checkListStorage.GetElement(model) };
            }
            return _checkListStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(InterimReportBindingModel model)
        {
            var element = _checkListStorage.GetElement(new InterimReportBindingModel
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
                _checkListStorage.Update(model);
            }
            else
            {
                _checkListStorage.Insert(model);
            }
        }
        public void Delete(InterimReportBindingModel model)
        {
            var element = _checkListStorage.GetElement(new InterimReportBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _checkListStorage.Delete(model);
        }
    }
}
