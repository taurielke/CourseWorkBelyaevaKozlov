using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentLogic logic;
        public StudentController(StudentLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public List<StudentViewModel> GetStudentList() => logic.Read(null)?.ToList();

        [HttpGet]
        public List<StudentViewModel> GetStudents(int deaneryId) => logic.Read(new StudentBindingModel { DeaneryId = deaneryId });

        [HttpGet]
        public StudentViewModel GetStudent(int gradeBookNumber) => logic.Read(new StudentBindingModel { GradebookNumber = gradeBookNumber })?[0];

        [HttpPost]
        public void CreateOrUpdateStudent(StudentBindingModel model) => logic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteStudent(StudentBindingModel model) => logic.Delete(model);
    }
}
