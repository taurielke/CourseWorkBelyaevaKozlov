using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniversityDeaneryApp.Models;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using UniversityBusinessLogic.Mail;


namespace UniversityDeaneryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly MailKitWorker _mailKitWorker;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, MailKitWorker mailKitWorker)
        {
            _logger = logger;
            _environment = environment;
            _mailKitWorker = mailKitWorker;
        }
        public IActionResult Index()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
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
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIDeanery.GetRequest<List<AttestationViewModel>>($"api/attestation/GetAttestations?deaneryId={Program.Deanery.Id}"));
        }

        [HttpGet]
        public IActionResult AttestationCreate()
        {
            ViewBag.Students = APIDeanery.GetRequest<List<StudentViewModel>>($"api/student/GetStudents?deaneryId={Program.Deanery.Id}");
            return View();
        }

        [HttpPost]
        public void AttestationCreate(int gradebookNumber, int semesterNumber)
        {
            if (gradebookNumber != 0 && semesterNumber != 0)
            {
                APIDeanery.PostRequest("api/attestation/CreateOrUpdateAttestation", new AttestationBindingModel
                {
                    SemesterNumber = semesterNumber,
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
            ViewBag.Attestation = APIDeanery.GetRequest<AttestationViewModel>($"api/attestation/GetAttestation?attestationId={attestationId}");
            ViewBag.Students = APIDeanery.GetRequest<List<StudentViewModel>>($"api/student/GetStudents?deaneryId={Program.Deanery.Id}");
            return View();
        }

        [HttpPost]
        public void AttestationUpdate(int attestationId, int gradebookNumber, int semesterNumber)
        {
            if (attestationId !=0 && gradebookNumber!=0 && semesterNumber !=0)
            {
                var attestation = APIDeanery.GetRequest<AttestationViewModel>($"api/attestation/GetAttestation?attestationId={attestationId}");
                if (attestation == null)
                {
                    return;
                }
                APIDeanery.PostRequest("api/attestation/CreateOrUpdateAttestation", new AttestationBindingModel
                {
                    Id = attestation.Id,
                    SemesterNumber = semesterNumber,
                    StudentId = gradebookNumber,
                    Date = DateTime.Now, 
                    DeaneryId= Program.Deanery.Id
                });
                Response.Redirect("Attestation");
                return;
            }
            throw new Exception("Выберите студента");
        }

        [HttpGet]
        public void AttestationDelete(int attestationId)
        {
            var attestation = APIDeanery.GetRequest<AttestationViewModel>($"api/attestation/GetAttestation?attestationId={attestationId}");
            APIDeanery.PostRequest("api/attestation/DeleteAttestation", attestation);
            Response.Redirect("Attestation");
        }

        public IActionResult Student()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIDeanery.GetRequest<List<StudentViewModel>>($"api/student/GetStudents?deaneryId={Program.Deanery.Id}"));
 
        }

        [HttpGet]
        public IActionResult StudentCreate()
        {
            return View();
        }

        [HttpPost]
        public void StudentCreate(string name, string streamName)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(streamName))
            {
                APIDeanery.PostRequest("api/student/CreateOrUpdateStudent", new StudentBindingModel
                {
                    Name = name,
                    StreamName = streamName,
                    LearningPlans = new Dictionary<int, string>(),
                    Disciplines = new Dictionary<int, string>(),
                    DeaneryId = Program.Deanery.Id
                });
                Response.Redirect("Student");
                return;
            }
            throw new Exception("Заполните ФИО!");
        }

        [HttpGet]
        public IActionResult StudentUpdate(int gradebookNumber)
        {
            ViewBag.Student = APIDeanery.GetRequest<StudentViewModel>($"api/student/GetStudent?gradebookNumber={gradebookNumber}") ;
            return View();
        }

        [HttpPost]
        public void StudentUpdate(int gradebookNumber, string name, string streamName)
        {
            if (gradebookNumber != 0 && !string.IsNullOrEmpty(name))
            {
                var student = APIDeanery.GetRequest<StudentViewModel>($"api/student/GetStudent?gradebookNumber={gradebookNumber}");
                if (student == null)
                {
                    return;
                }
                APIDeanery.PostRequest("api/student/CreateOrUpdateStudent", new StudentBindingModel
                {
                    GradebookNumber = gradebookNumber,
                    StreamName=streamName,
                    Name=name,
                    LearningPlans = student.LearningPlans,
                    Disciplines = student.Disciplines,
                    DeaneryId = Program.Deanery.Id
                });
                Response.Redirect("Student");
                return;
            }
            throw new Exception("Заполните все поля");
        }

        [HttpGet]
        public void StudentDelete(int gradebookNumber)
        {
            var student = APIDeanery.GetRequest<StudentViewModel>($"api/student/GetStudent?gradebookNumber={gradebookNumber}");
            APIDeanery.PostRequest("api/student/DeleteStudent", student);
            Response.Redirect("Student");
        }

        public IActionResult LearningPlan()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIDeanery.GetRequest<List<LearningPlanViewModel>>($"api/LearningPlan/GetLearningPlans?deaneryId={Program.Deanery.Id}"));
        }

        [HttpGet]
        public IActionResult LearningPlanCreate()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Teachers = APIDeanery.GetRequest<List<TeacherViewModel>>("api/learningPlan/GetTeacherList");
            return View();
        }

        [HttpPost]
        public void LearningPlanCreate(string learningPlanName, int hours, List <int> teachersId)
        {
            List<TeacherViewModel> teachers = new List<TeacherViewModel>();
            foreach(var teacherId in teachersId)
            {
                teachers.Add(APIDeanery.GetRequest<TeacherViewModel>($"api/learningPlan/GetTeacher?teacherId={teacherId}"));
            }

            if ( !string.IsNullOrEmpty(learningPlanName) && hours !=0 && teachers!=null)
            {
                APIDeanery.PostRequest("api/LearningPlan/CreateOrUpdateLearningPlan", new LearningPlanBindingModel
                {
                    LearningPlanName =  learningPlanName,
                    Hours = hours,
                    Teachers = teachers.ToDictionary(x => x.Id, x=>x.Name),
                    DeaneryId = Program.Deanery.Id
                });
                Response.Redirect("LearningPlan");
                return;
            }
            throw new Exception("Выберите преподавателей и заполните все поля");
        }

        [HttpGet]
        public IActionResult LearningPlanUpdate(int learningPlanId)
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.LearningPlan = APIDeanery.GetRequest<LearningPlanViewModel>($"api/LearningPlan/GetLearningPlan?learningPlanId={learningPlanId}");
            ViewBag.Teachers = APIDeanery.GetRequest<List<TeacherViewModel>>("api/learningPlan/GetTeacherList");
            return View();
        }

        [HttpPost]
        public void LearningPlanUpdate(int learningPlanId, string learningPlanName, int hours, List<int> teachersId)
        {
            List<TeacherViewModel> teachers = new List<TeacherViewModel>();
            foreach (var teacherId in teachersId)
            {
                teachers.Add(APIDeanery.GetRequest<TeacherViewModel>($"api/learningPlan/GetTeacher?teacherId={teacherId}"));
            }

            if (learningPlanId != 0 && !string.IsNullOrEmpty(learningPlanName) && hours != 0 && teachers != null)
            {
                var learningPlan = APIDeanery.GetRequest<LearningPlanViewModel>($"api/LearningPlan/GetLearningPlan?LearningPlanId={learningPlanId}");
                if (learningPlan == null)
                {
                    return;
                }
                APIDeanery.PostRequest("api/learningPlan/CreateOrUpdateLearningPlan", new LearningPlanBindingModel
                {
                    Id = learningPlan.Id,
                    LearningPlanName = learningPlanName,
                    Hours = hours,
                    Teachers = teachers.ToDictionary(x => x.Id, x => x.Name),
                    DeaneryId = Program.Deanery.Id
                });
                Response.Redirect("LearningPlan");
                return;
            }
            throw new Exception("Заполните все поля и выберете преподавателей");
        }

        [HttpGet]
        public void LearningPlanDelete(int learningPlanId)
        {
            var learningPlan = APIDeanery.GetRequest<LearningPlanViewModel>($"api/learningPlan/GetLearningPlan?learningPlanId={learningPlanId}");
            APIDeanery.PostRequest("api/learningPlan/DeleteLearningPlan", learningPlan);
            Response.Redirect("LearningPlan");
        }

        [HttpGet]
        public IActionResult BindStudentLearningPlans() 
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Students = APIDeanery.GetRequest<List<StudentViewModel>>($"api/Student/GetStudents?deaneryId={Program.Deanery.Id}");
            ViewBag.LearningPlans = APIDeanery.GetRequest<List<LearningPlanViewModel>>($"api/LearningPlan/GetLearningPlans?deaneryId={Program.Deanery.Id}");
            return View();
        }

        [HttpPost]
        public void BindStudentLearningPlans(int studentId, List<int> learningPlansId) 
        {
            if (studentId != 0 && learningPlansId !=null)
            {
                APIDeanery.PostRequest("api/Student/BindStudentLearningPlans", new AddStudentToLearningPlanBindingModel
                {
                    GradebookNumber = studentId,
                    LearningPlansId = learningPlansId
                });
                Response.Redirect("Student");
                return;
            }
            throw new Exception("Заполните все поля и выберете преподавателей");
        }






        public IActionResult ReportWordExcel()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.LearningPlans = APIDeanery.GetRequest<List<LearningPlanViewModel>>($"api/LearningPlan/GetLearningPlans?deaneryId={Program.Deanery.Id}");
            return View();
        }

        [HttpPost]
        public IActionResult CreateReportLearningPlanDisciplinesToWordFile(List<int> learningPlansId)
        {
            if (learningPlansId != null)
            {
                var model = new ReportBindingModel
                {
                    LearningPlans = new List<LearningPlanViewModel>(),
                    Disciplines = new List<DisciplineViewModel>()
                };
                foreach (var learningPlanId in learningPlansId)
                {
                    model.LearningPlans.Add(APIDeanery.GetRequest<LearningPlanViewModel>($"api/LearningPlan/GetLearningPlan?learningPlanId={learningPlanId}"));
                }
                model.FileName = @"..\UniversityStudentApp\wwwroot\ReportLearningPlanDisciplines\ReportLearningPlanDisciplinesDoc.doc";
                APIDeanery.PostRequest("api/report/CreateReportLearningPlanDisciplinesToWordFile", model);
                var fileName = "ReportLearningPlanDisciplinesDoc.doc";
                var filePath = _environment.WebRootPath + @"\ReportLearningPlanDisciplines\" + fileName;
                return PhysicalFile(filePath, "application/doc", fileName);
            }
            throw new Exception("Выберите хотя бы один план обучения");
        }

        [HttpPost]
        public IActionResult CreateReportLearningPlanDisciplinesToExcelFile(List<int> learningPlansId)
        {
            if (learningPlansId != null)
            {
                var model = new ReportBindingModel
                {
                    LearningPlans = new List<LearningPlanViewModel>(),
                    Disciplines = new List<DisciplineViewModel>()
                };
                foreach (var learningPlanId in learningPlansId)
                {
                    model.LearningPlans.Add(APIDeanery.GetRequest<LearningPlanViewModel>($"api/LearningPlan/GetLearningPlan?learningPlanId={learningPlanId}"));
                }
                model.FileName = @"..\UniversityStudentApp\wwwroot\ReportLearningPlanDisciplines\ReportLearningPlanDisciplinesExcel.xls";
                APIDeanery.PostRequest("api/report/CreateReportLearningPlanDisciplinesToExcelFile", model);
                var fileName = "ReportLearningPlanDisciplinesExcel.xls";
                var filePath = _environment.WebRootPath + @"\ReportLearningPlanDisciplines\" + fileName;
                return PhysicalFile(filePath, "application/xls", fileName);
            }
            throw new Exception("Выберите хотя бы один план обучения");
        }

        public IActionResult ReportPDF()
        {
            if (Program.Deanery == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ReportAttestationsPDF(DateTime dateFrom, DateTime dateTo)
        {
            ViewBag.Period = "C " + dateFrom.ToLongDateString() + " по " + dateTo.ToLongDateString();
            ViewBag.Report = APIDeanery.GetRequest<List<ReportAttestationsViewModel>>($"api/report/GetAttestationsReport?dateFrom={dateFrom.ToLongDateString()}&dateTo={dateTo.ToLongDateString()}");
            return View("ReportPdf");
        }

        public IActionResult SendReportOnMail(DateTime dateFrom, DateTime dateTo)
        {
            var model = new ReportBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            model.FileName = @"..\UniversityStudentApp\wwwroot\ReportAttestations\ReportAttestationsPdf.pdf";
            APIDeanery.PostRequest("api/report/CreateReportAttestationsToPdfFile", model);
            _mailKitWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = Program.Deanery.Login,
                Subject = "Отчет по аттестациям. Университет \"Все отчислены\"",
                Text = "Отчет по аттестациям с " + dateFrom.ToShortDateString() + " по " + dateTo.ToShortDateString() +
                "\nДеканат - " + Program.Deanery.Name,
                FileName = model.FileName,
            });
            return Redirect("~/Home/Index");
        }

    }
}