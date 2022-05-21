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
        private readonly int _passwordMaxLength = 50;
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
            if (model.Id.HasValue)
            {
                return new List<DeaneryViewModel> { _deaneryStorage.GetElement(model) };
            }
            return _deaneryStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DeaneryBindingModel model)
        {
            var element = _deaneryStorage.GetElement(new DeaneryBindingModel
            {
                Email = model.Email
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть деканат с таким логином");
            }
            if (!Regex.IsMatch(model.Email, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength
                || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен состоять и из цифр, букв и небуквенных символов");
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
