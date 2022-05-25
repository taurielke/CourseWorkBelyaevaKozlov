using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IReportLogic
    {
        List<ReportLearningPlanDisciplinesViewModel> GetLearningPlanDisciplines(ReportBindingModel model);
        void SaveLearningPlanDisciplinesToWordFile(ReportBindingModel model);
        void SaveLearningPlanDisciplinesToExcelFile(ReportBindingModel model);
        List<ReportAttestationsViewModel> GetAttestations(ReportBindingModel model);
        void SaveAttestationsToPdfFile(ReportBindingModel model);
    }
}
