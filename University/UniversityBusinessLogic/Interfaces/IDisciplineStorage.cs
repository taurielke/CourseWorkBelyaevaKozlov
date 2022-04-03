using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IDisciplineStorage
    {
        List<DisciplineViewModel> GetFullList();
        List<DisciplineViewModel> GetFilteredList(DisciplineBindingModel model);
        DisciplineViewModel GetElement(DisciplineBindingModel model);
        void Insert(DisciplineBindingModel model);
        void Update(DisciplineBindingModel model);
        void Delete(DisciplineBindingModel model);
    }
}
