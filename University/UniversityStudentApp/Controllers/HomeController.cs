using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniversityDeaneryApp.Models;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace UniversityDeaneryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIDeanery.GetRequest<List<LearningPlanViewModel>>($"api/learningPlan/getlearningplans?deaneryId={Program.Deanery.Id}"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Deanery = APIDeanery.GetRequest<DeaneryViewModel>($"api/deanery/login?login={login}&password={password}");
                if (Program.Deanery == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string password, string deaneryName)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(deaneryName))
            {
                APIDeanery.PostRequest("api/deanery/register", new DeaneryBindingModel
                {
                    Name = deaneryName,
                    Login = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и название деканата");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Deanery);
        }

        [HttpPost]
        public void Privacy(string login, string password, string name)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(name))
            {
                APIDeanery.PostRequest("api/deanery/updatedata", new DeaneryBindingModel
                {
                    Id = Program.Deanery.Id,
                    Name = name,
                    Login = login,
                    Password = password
                });
                Program.Deanery.Name = name;
                Program.Deanery.Login = login;
                Program.Deanery.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        public IActionResult Attestation()
        {
            return View(APIDeanery.GetRequest<List<AttestationViewModel>>("api/attestation/GetAttestations?deaneryId={Program.Deanery.Id}"));
        }

        [HttpGet]
        public IActionResult AttestationCreate()
        {
            ViewBag.Students = APIDeanery.GetRequest<List<StudentViewModel>>("api/student/GetStudents?deaneryId={Program.Deanery.Id}");
            return View();
        }

        [HttpPost]
        public void AttestationCreate(int gradebookNumber)
        {
            if (gradebookNumber != 0)
            {
                APIDeanery.PostRequest("api/attestation/CreateOrUpdateAttestation", new AttestationBindingModel
                {
                    StudentId = gradebookNumber,
                    Date = DateTime.Now,
                    DeaneryId = Program.Deanery.Id
                });
                Response.Redirect("Attestation");
                return;
            }
            throw new Exception("Выберите студента");
        }

        [HttpGet]
        public IActionResult AttestationUpdate(int attestationId)
        {
            ViewBag.Attestation = APIDeanery.GetRequest<List<AttestationViewModel>>("api/attestation/GetAttestation?attestationId={attestationId}");
            ViewBag.Students = APIDeanery.GetRequest<List<StudentViewModel>>("api/student/GetStudents?deaneryId={Program.Deanery.Id}");
            return View();
        }

        [HttpPost]
        public void AttestationUpdate(int attestationId, int gradebookNumber)
        {
            if (attestationId !=0 && gradebookNumber!=0)
            {
                var attestation = APIDeanery.GetRequest<AttestationViewModel>($"api/attestation/GetAttestation?attestationId={attestationId}");
                if (attestation == null)
                {
                    return;
                }
                APIDeanery.PostRequest("api/attestation/CreateOrUpdateAttestation", new AttestationBindingModel
                {
                    Id = attestation.Id,
                    StudentId = gradebookNumber,
                    Date = DateTime.Now, 
                    DeaneryId= Program.Deanery.Id
                });
                Response.Redirect("Attestation");
                return;
            }
            throw new Exception("Выберите студента");
        }

        [HttpPost]
        public void AttestationDelete(int attestationId)
        {
            var attestation = APIDeanery.GetRequest<AttestationViewModel>($"api/attestation/GetAttestation?attestationId={attestationId}");
            APIDeanery.PostRequest("api/attestation/DeleteAttestation", attestation);
            Response.Redirect("Index");
        }



    }
}