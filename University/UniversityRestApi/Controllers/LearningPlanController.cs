using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LearningPlanController : ControllerBase
    {
        private readonly LearningPlanLogic logic;
        private readonly DisciplineLogic dLogic;
        public LearningPlanController(LearningPlanLogic logic, DisciplineLogic dLogic)
        {
            this.logic = logic;
            this.dLogic = dLogic;
        }

        [HttpGet]
        public List<DisciplineViewModel> GetDisciplineList() => dLogic.Read(null)?.ToList();

        [HttpGet]
        public List<LearningPlanViewModel> GetLearningPlanList() => logic.Read(null)?.ToList();

        [HttpGet]
        public List<LearningPlanViewModel> GetLearningPlans(int deaneryId) => logic.Read(new LearningPlanBindingModel { DeaneryId = deaneryId });

        [HttpGet]
        public LearningPlanViewModel GetLearningPlan(int learningPlanId) => logic.Read(new LearningPlanBindingModel { Id = learningPlanId })?[0];

        [HttpPost]
        public void CreateOrUpdateLearningPlan(LearningPlanBindingModel model) => logic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteLearningPlan(LearningPlanBindingModel model) => logic.Delete(model);
    }
}
