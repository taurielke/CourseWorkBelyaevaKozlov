using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogic
{
    public class AttestationLogic
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
                return new List<AttestationViewModel>
                {
                    _attestationStorage.GetElement(model)
                };
            }
            return _attestationStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(AttestationBindingModel model)
        {
            var element = _attestationStorage.GetElement(new AttestationBindingModel
            {
                RecordBookNumber = model.RecordBookNumber,
                SemesterNumber = model.SemesterNumber,
                DisciplineId = model.DisciplineId
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Аттестация по этой дисциплине уже занесена!");
            }
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
            var element = _attestationStorage.GetElement(new AttestationBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Аттестация не найдена");
            }
            _attestationStorage.Delete(model);
        }
    }
}
