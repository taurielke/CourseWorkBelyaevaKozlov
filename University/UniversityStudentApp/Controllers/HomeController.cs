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
        public IActionResult Index()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIDeanery.GetRequest<List<LearningPlanViewModel>>($"api/main/getlearningplans?deaneryId={Program.Deanery.Id}"));
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
                APIDeanery.PostRequest("api/Deanery/register", new DeaneryBindingModel
                {
                    DeaneryName= deaneryName,
                    Email = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

    }
}