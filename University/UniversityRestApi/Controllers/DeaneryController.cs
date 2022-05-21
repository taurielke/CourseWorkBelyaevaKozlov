using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic _logic;
        public StudentController(IStudentLogic logic)
        {
            _logic = logic;
        }
        //we don't need to let them login so this is probably useless
        [HttpGet]
        public StudentViewModel Login(string login, string password)
        {
            var list = _logic.Read(new StudentBindingModel
            {
                Email = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }
        [HttpPost]
        public void Register(StudentBindingModel model) => _logic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(StudentBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
