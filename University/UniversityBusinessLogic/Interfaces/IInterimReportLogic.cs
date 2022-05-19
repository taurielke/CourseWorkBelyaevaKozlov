using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IInterimReportLogic
    {
        List<InterimReportViewModel> Read(InterimReportBindingModel model);
        void CreateOrUpdate(InterimReportBindingModel model);
        void Delete(InterimReportBindingModel model);
    }
}
