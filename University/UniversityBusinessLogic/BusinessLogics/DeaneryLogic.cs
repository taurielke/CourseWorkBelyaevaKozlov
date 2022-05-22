using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class DeaneryLogic : IDeaneryLogic
    {
        private readonly IDeaneryStorage _deaneryStorage;
        public DeaneryLogic(IDeaneryStorage deaneryStorage)
        {
            _deaneryStorage = deaneryStorage;
        }
        public List<DeaneryViewModel> Read(DeaneryBindingModel model)
        {
            if (model == null)
            {
                return _deaneryStorage.GetFullList();
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                return new List<DeaneryViewModel> { _deaneryStorage.GetElement(model) };
            }
            return _deaneryStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DeaneryBindingModel model)
        {
            var element = _deaneryStorage.GetElement(new DeaneryBindingModel
            {
                Login = model.Login
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть деканат с таким логином");
            }
            if (model.Id.HasValue)
            {
                _deaneryStorage.Update(model);
            }
            else
            {
                _deaneryStorage.Insert(model);
            }
        }
        public void Delete(DeaneryBindingModel model)
        {
            var element = _deaneryStorage.GetElement(new DeaneryBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый деканат не найден");
            }
            _deaneryStorage.Delete(model);
        }
    }
}
