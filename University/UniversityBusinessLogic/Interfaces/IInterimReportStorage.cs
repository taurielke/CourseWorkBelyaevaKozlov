using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IInterimReportStorage
    {
        List<InterimReportViewModel> GetFullList();
        List<InterimReportViewModel> GetFilteredList(InterimReportBindingModel model);
        InterimReportViewModel GetElement(InterimReportBindingModel model);
        void Insert(InterimReportBindingModel model);
        void Update(InterimReportBindingModel model);
        void Delete(InterimReportBindingModel model);
    }
}
