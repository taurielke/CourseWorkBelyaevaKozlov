using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IReportLogic
    {
        List<ReportDisciplinesInLearningPlansViewModel> GetLearningPlanDiscipline();
        List<ReportAttestationViewModel> GetAttestations(ReportBindingModel model);
        void SaveDisciplinesInLearningPlansToWordFile(ReportBindingModel model);
        void SaveDisciplinesInLearningPlansToExcelFile(ReportBindingModel model);
        void SaveAttestationToPdfFile(ReportBindingModel model);
    }
}
