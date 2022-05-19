using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IAttestationLogic
    {
        List<AttestationViewModel> Read(AttestationBindingModel model);
        void CreateOrUpdate(AttestationBindingModel model);
        void Delete(AttestationBindingModel model);
    }
}
