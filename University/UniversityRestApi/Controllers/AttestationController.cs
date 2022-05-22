using Microsoft.AspNetCore.Mvc;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels; 

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttestationController : ControllerBase
    {
        private readonly IAttestationLogic logic;
        public AttestationController(IAttestationLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public List<AttestationViewModel> GetAttestationList() => logic.Read(null)?.ToList();

        [HttpGet]
        public List<AttestationViewModel> GetAttestations(int deaneryId) => logic.Read(new AttestationBindingModel { DeaneryId = deaneryId });

        [HttpGet]
        public AttestationViewModel GetAttestation(int attestationId) => logic.Read(new AttestationBindingModel { Id = attestationId })?[0];

        [HttpPost]
        public void CreateOrUpdateAttestation(AttestationBindingModel model) => logic.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteAttestation(AttestationBindingModel model) => logic.Delete(model);
    }
}
