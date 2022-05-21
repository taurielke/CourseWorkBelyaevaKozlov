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

        public MainController(IAttestationLogic attestationlogic, ILearningPlanLogic learningPlanLogic, IStudentLogic studentLogic)
        {
            this.studentLogic = studentLogic;
            this.learningPlanLogic = learningPlanLogic;
            this.attestationLogic = attestationlogic;
        }
        /* [HttpGet]
         public List<AttestationViewModel> GetAttestationList() => logic.Read(null)?.ToList();
         [HttpGet]
         public FurnitureViewModel GetFurniture(int furnitureId) => _furniture.Read(new FurnitureBindingModel { Id = furnitureId })?[0];*/
        /*[HttpPost]
         public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);*/

        [HttpGet]
        public List<LearningPlanViewModel> GetLearningPlans(int deaneryId) => learningPlanLogic.Read(new LearningPlanBindingModel { DeaneryId = deaneryId });
        [HttpGet]
        public List<StudentViewModel> GetStudents(int learningPlanId) => studentLogic.Read(new StudentBindingModel { LearningPlanId = learningPlanId });
        [HttpGet]
        public List<AttestationViewModel> GetAttestations(int recordBookNumber) => attestationLogic.Read(new AttestationBindingModel { RecordBookNumber = recordBookNumber });
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
