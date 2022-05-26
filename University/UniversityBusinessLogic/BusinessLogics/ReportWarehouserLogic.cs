using System;
using System.Collections.Generic;
using System.Linq;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.OfficePackage.HelperModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.OfficePackage.Implements;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class ReportWarehouserLogic
    {
        private readonly ITeacherStorage _teacherStorage;
        private readonly IInterimReportStorage _interimReportStorage;
        private readonly IStudentStorage _studentStorage;
        private readonly IDisciplineStorage _disciplineStorage;
        public ReportWarehouserLogic(ITeacherStorage teacherStorage, IInterimReportStorage interimReportStorage, IStudentStorage studentStorage, IDisciplineStorage disciplineStorage)
        {
            _teacherStorage = teacherStorage;
            _interimReportStorage = interimReportStorage;
            _studentStorage = studentStorage;
            _disciplineStorage = disciplineStorage;
        }
        public List<ReportDisciplineStudentViewModel> GetDisciplineStudent()
        {
            var students = _studentStorage.GetFullList();
            var list = new List<ReportDisciplineStudentViewModel>();
            foreach (var student in students)
            {
                var record = new ReportDisciplineStudentViewModel
                {
                    StudentName = student.Name,
                    Disciplines = new List<string>(),
                };
                foreach (var discipline in student.Disciplines)
                {
                    record.Disciplines.Add(discipline.Value);
                }
                list.Add(record);
            }
            return list;
        }
        public List<ReportInterimReportViewModel> GetInterimReports(ReportWarehouserBindingModel model)
        {
            return _interimReportStorage.GetByDiscipline(model.DateFrom, model.DateTo, model.DisciplineId);
        }
        public void SaveTeacherStudentsToWordFile(ReportWarehouserBindingModel model, List<StudentViewModel> students)
        {
            SaveToWordWarehouser.CreateDoc(new WordInfoWarehouser
            {
                FileName = model.FileName,
                TeacherName = _teacherStorage.GetElement(new TeacherBindingModel
                {
                    Id = model.TeacherId
                }).Name,
                Title = "Список студентов",
                Students = students
            });
        }
        public void SaveTeacherStudentToExcelFile(ReportWarehouserBindingModel model, List<StudentViewModel> students)
        {
            SaveToExcelWarehouser.CreateDoc(new ExcelInfoWarehouser
            {
                FileName = model.FileName,
                TeacherName = _teacherStorage.GetElement(new TeacherBindingModel
                {
                    Id = model.TeacherId
                }).Name,
                Title = "Список студентов",
                Students = students
            });
        }

        [Obsolete]
        public void SaveInterimReportsByDateByDisciplineToPdfFile(ReportWarehouserBindingModel model)
        {
            SaveToPdfWarehouser.CreateDoc(new PdfInfoWarehouser
            {
                FileName = model.FileName,
                Title = "Отчёт по дисциплине",
                InterimReports = GetInterimReports(new ReportWarehouserBindingModel
                {
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    DisciplineId = model.DisciplineId
                }),
                DisciplineName = _disciplineStorage.GetElement(new DisciplineBindingModel
                {
                    Id = model.DisciplineId
                }).Name,
                DateFrom = (DateTime)model.DateFrom,
                DateTo = (DateTime)model.DateTo,
            });
        }
    }
}
