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
        private readonly IDeaneryStorage _denearyStorage;
        public DeaneryLogic(IDeaneryStorage denearyStorage)
        {
            _denearyStorage = denearyStorage;
        }
        public List<DeaneryViewModel> Read(DeaneryBindingModel model)
        {
            if (model == null)
            {
                return _denearyStorage.GetFullList();
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                return new List<DeaneryViewModel> { _denearyStorage.GetElement(model) };
            }
            return _denearyStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DeaneryBindingModel model)
        {
            var element = _denearyStorage.GetElement(new DeaneryBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.Login != model.Login)
            {
                throw new Exception("Уже есть такой логин");
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                _denearyStorage.Update(model);
            }
            else
            {
                _denearyStorage.Insert(model);
            }
        }
        public void Delete(DeaneryBindingModel model)
        {
            var element = _denearyStorage.GetElement(new DeaneryBindingModel { Login = model.Login });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _denearyStorage.Delete(model);
        }
    }
}
