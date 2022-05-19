using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IDisciplineLogic
    {
        List<DisciplineViewModel> Read(DisciplineBindingModel model);
        void CreateOrUpdate(DisciplineBindingModel model);
        void Delete(DisciplineBindingModel model);
    }
}
