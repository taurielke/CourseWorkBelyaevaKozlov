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
        private readonly IAttestationStorage _attestationStorage;
        public AttestationLogic(IAttestationStorage attestationStorage)
        {
            _attestationStorage = attestationStorage;
        }
        public List<AttestationViewModel> Read(AttestationBindingModel model)
        {
            if (model == null)
            {
                return _attestationStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<AttestationViewModel> { _attestationStorage.GetElement(model) };
            }
            return _attestationStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(AttestationBindingModel model)
        {
            
            if (model.Id.HasValue)
            {
                _attestationStorage.Update(model);
            }
            else
            {
                _attestationStorage.Insert(model);
            }
        }
        public void Delete(AttestationBindingModel model)
        {
            var element = _attestationStorage.GetElement(new AttestationBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _attestationStorage.Delete(model);
        }
    }
}
