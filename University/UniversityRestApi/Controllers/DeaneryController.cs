using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeaneryController : ControllerBase
    {
        private readonly IDeaneryLogic _logic;
        public DeaneryController(IDeaneryLogic logic)
        {
            _logic = logic;
        }
        [HttpGet]
        public DeaneryViewModel Login(string login, string password)
        {
            var list = _logic.Read(new DeaneryBindingModel
            {
                Login = login,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }
        [HttpPost]
        public void Register(DeaneryBindingModel model) => _logic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(DeaneryBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
