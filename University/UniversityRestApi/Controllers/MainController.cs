using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ILearningPlanLogic learningPlanLogic;
        private readonly IAttestationLogic attestationLogic;
        private readonly IStudentLogic studentLogic;
        private readonly IDisciplineLogic disciplineLogic;

        public MainController(IAttestationLogic attestationLogic, ILearningPlanLogic learningPlanLogic, IStudentLogic studentLogic, IDisciplineLogic disciplineLogic)
        {
            this.studentLogic = studentLogic;
            this.learningPlanLogic = learningPlanLogic;
            this.attestationLogic = attestationLogic;
            this.disciplineLogic = disciplineLogic;
        }
        /* [HttpGet]
         public List<AttestationViewModel> GetAttestationList() => logic.Read(null)?.ToList();
         [HttpGet]
         public FurnitureViewModel GetFurniture(int furnitureId) => _furniture.Read(new FurnitureBindingModel { Id = furnitureId })?[0];*/
        /*[HttpPost]
         public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);*/
        [HttpGet]
        public List<DisciplineViewModel> GetDisciplineList() => disciplineLogic.Read(null)?.ToList();
        [HttpGet]
        public List<LearningPlanViewModel> GetLearningPlans(int deaneryId) => learningPlanLogic.Read(new LearningPlanBindingModel { DeaneryId = deaneryId });
        [HttpGet]
        public List<StudentViewModel> GetStudents(int deaneryId) => studentLogic.Read(new StudentBindingModel { DeaneryId = deaneryId });
        [HttpGet]
        public List<AttestationViewModel> GetAttestations(int deaneryId) => attestationLogic.Read(new AttestationBindingModel { DeaneryId = deaneryId });
        [HttpPost]
        public void CreateOrUpdateLearningPlan(LearningPlanBindingModel model) => learningPlanLogic.CreateOrUpdate(model);
        [HttpPost]
        public void CreateOrUpdateStudent(StudentBindingModel model) => studentLogic.CreateOrUpdate(model);
        [HttpPost]
        public void CreateOrUpdateAttestation(AttestationBindingModel model) => attestationLogic.CreateOrUpdate(model);
        [HttpDelete]
        public void DeleteLearningPlan(LearningPlanBindingModel model) => learningPlanLogic.Delete(model);
        [HttpDelete]
        public void DeleteStudent(StudentBindingModel model) => studentLogic.Delete(model);
        [HttpDelete]
        public void DeleteAttestation(AttestationBindingModel model) => attestationLogic.Delete(model);
    }
}
