using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportLogic _reportLogic;
        private readonly ILearningPlanLogic _learningPlanLogic;
        private readonly IAttestationLogic _attestationLogic;
        public ReportController(IReportLogic reportLogic, ILearningPlanLogic learningPlanLogic, IAttestationLogic attestationLogic)
        {
            _reportLogic = reportLogic;
            _learningPlanLogic = learningPlanLogic;
            _attestationLogic = attestationLogic;
        }

        [HttpPost]
        public void CreateReportLearningPlanDisciplinesToWordFile(ReportBindingModel model) => _reportLogic.SaveLearningPlanDisciplinesToWordFile(model);

        [HttpPost]
        public void CreateReportLearningPlanDisciplinesToExcelFile(ReportBindingModel model) => _reportLogic.SaveLearningPlanDisciplinesToExcelFile(model);

        [HttpPost]
        public void CreateReportAttestationsToPdfFile(ReportBindingModel model) => _reportLogic.SaveAttestationsToPdfFile(model);

        [HttpGet]
        public ReportBindingModel GetLearningPlansForReport(int deaneryId)
        {
            return new ReportBindingModel
            {
                DeaneryId = deaneryId,
                LearningPlans = _learningPlanLogic.Read(new LearningPlanBindingModel { DeaneryId = deaneryId })
            };
        }

        [HttpGet]
        public ReportBindingModel GetAttestationsForReport(int deaneryId)
        {
            return new ReportBindingModel
            {
                DeaneryId = deaneryId,
                Attestations = _attestationLogic.Read(new AttestationBindingModel { DeaneryId = deaneryId })
            };
        }
    }
}
