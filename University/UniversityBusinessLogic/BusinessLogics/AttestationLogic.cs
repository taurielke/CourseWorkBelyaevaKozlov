using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class AttestationLogic : IAttestationLogic
    {
        private readonly IAttestationStorage _certificationStorage;
        public AttestationLogic(IAttestationStorage certificationStorage)
        {
            _certificationStorage = certificationStorage;
        }
        public List<AttestationViewModel> Read(AttestationBindingModel model)
        {
            if (model == null)
            {
                return _certificationStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<AttestationViewModel> { _certificationStorage.GetElement(model) };
            }
            return _certificationStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(AttestationBindingModel model)
        {
            var element = _certificationStorage.GetElement(new AttestationBindingModel
            {
                Date = model.Date,
                StudentId = model.StudentId,
                DenearyLogin = model.DenearyLogin
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть студент с таким именем");
            }
            if (model.Id.HasValue)
            {
                _certificationStorage.Update(model);
            }
            else
            {
                _certificationStorage.Insert(model);
            }
        }
        public void Delete(AttestationBindingModel model)
        {
            var element = _certificationStorage.GetElement(new AttestationBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _certificationStorage.Delete(model);
        }
    }
}
