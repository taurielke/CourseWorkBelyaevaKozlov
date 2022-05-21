using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniversityStudentApp.Models;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using UniversityStudentApp;

namespace UniversityStudentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Attestation()
        {
            if (Program.Student == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIDeanery.GetRequest<List<AttestationViewModel>>($"api/main/getattestations?recordBookNumber={Program.Student.RecordBookNumber}"));
        }

        public IActionResult InterimReport()
        {
            if (Program.Student == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIDeanery.GetRequest<List<InterimReportViewModel>>($"api/main/getinterimreports?recordBookNumber={Program.Student.RecordBookNumber}"));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Student == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Student);
        }

        [HttpPost]
        public void Privacy(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                APIDeanery.PostRequest("api/student/updatedata", new StudentBindingModel
                {
                    RecordBookNumber = Program.Student.RecordBookNumber,
                    Email = login,
                    Password = password
                });
                Program.Student.Email = login;
                Program.Student.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
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
                Program.Student = APIDeanery.GetRequest<StudentViewModel>($"api/student/login?login={login}&password={password}");
                if (Program.Student == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        /*[HttpGet]
        public IActionResult CreateAttestationReport()
        {
            ViewBag.Attestations = APIStudent.GetRequest<List<AttestationViewModel>>("api/main/getattestations");
            return View();
        }

        [HttpPost]
        public void CreateAttestationReport(DateTime dateFrom, DateTime dateTo)
        {
            *//*if (count == 0 || sum == 0)
            {
                return;
            }*//*
            APIStudent.PostRequest("api/main/createorder", new CreateAttestationReportBindingModel
            {
                ClientId = Program.Client.Id,
                FlowerId = flower,
                Count = count,
                Sum = sum
            });
            Response.Redirect("Index");
        }*/

    }
}