using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System.Text.RegularExpressions;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class DeaneryLogic : IDeaneryLogic
    {
        private readonly IDeaneryStorage _deaneryStorage;
        private readonly int _emailMaxLength = 50;
        private readonly int _passwordMaxLength = 30;
        private readonly int _passwordMinLength = 5;
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
            if (model.Login.Length > _emailMaxLength || !Regex.IsMatch(model.Login, @"([a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+)"))
            {
                throw new Exception($"В качестве логина должна быть указана почта и иметь длинну не более {_emailMaxLength} символов");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength
                || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять из цифр, букв и небуквенных символов");
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
