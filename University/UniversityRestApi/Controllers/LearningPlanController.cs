using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LearningPlanController : ControllerBase
    {
        private readonly ILearningPlanLogic logic;
        private readonly ITeacherLogic dLogic;
        public LearningPlanController(ILearningPlanLogic logic, ITeacherLogic dLogic)
        {
            this.logic = logic;
            this.dLogic = dLogic;
        }

        [HttpGet]
        public List<TeacherViewModel> GetTeacherList() => dLogic.Read(null)?.ToList();
        [HttpGet]
        public TeacherViewModel GetTeacher(int teacherId) => dLogic.Read(new TeacherBindingModel { Id = teacherId })?[0];

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
