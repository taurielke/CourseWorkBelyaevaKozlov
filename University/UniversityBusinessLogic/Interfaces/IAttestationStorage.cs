using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IAttestationStorage
    {
        List<AttestationViewModel> GetFullList();
        List<AttestationViewModel> GetFilteredList(AttestationBindingModel model);
        AttestationViewModel GetElement(AttestationBindingModel model);
        void Insert(AttestationBindingModel model);
        void Update(AttestationBindingModel model);
        void Delete(AttestationBindingModel model);
    }
}
