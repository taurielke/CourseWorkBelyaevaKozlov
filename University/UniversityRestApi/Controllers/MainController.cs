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
        private readonly IAttestationLogic logic;
        private readonly IInterimReportLogic interimReportLogic;
        
        public MainController(IAttestationLogic logic, IInterimReportLogic interimReportLogic)
        {
            this.logic = logic;
            this.interimReportLogic = interimReportLogic;
        }
        /* [HttpGet]
         public List<AttestationViewModel> GetAttestationList() => logic.Read(null)?.ToList();
         [HttpGet]
         public FurnitureViewModel GetFurniture(int furnitureId) => _furniture.Read(new FurnitureBindingModel { Id = furnitureId })?[0];*/
        /*[HttpPost]
         public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);*/

        [HttpGet]
        public List<AttestationViewModel> GetAttestations(int recordBookNumber) => logic.Read(new AttestationBindingModel { RecordBookNumber = recordBookNumber });
        [HttpGet]
        public List<InterimReportViewModel> GetInterimReports(int recordBookNumber) => interimReportLogic.Read(new InterimReportBindingModel { RecordBookNumber = recordBookNumber });
        
    }
}
