using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IDeaneryStorage
    {
        List<DeaneryViewModel> GetFullList();

        List<DeaneryViewModel> GetFilteredList(DeaneryBindingModel model);

        DeaneryViewModel GetElement(DeaneryBindingModel model);

        void Insert(DeaneryBindingModel model);

        void Update(DeaneryBindingModel model);

        void Delete(DeaneryBindingModel model);
    }
}
