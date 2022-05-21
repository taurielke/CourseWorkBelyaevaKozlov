using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IDeaneryLogic
    {
        List<DeaneryViewModel> Read(DeaneryBindingModel model);
        void CreateOrUpdate(DeaneryBindingModel model);
        void Delete(DeaneryBindingModel model);
    }
}
