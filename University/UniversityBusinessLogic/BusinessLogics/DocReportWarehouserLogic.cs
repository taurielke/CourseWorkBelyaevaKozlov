using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.OfficePackage.HelperModels;
using UniversityBusinessLogic.OfficePackage.Implements;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class DocReportWarehouserLogic
    {
        private readonly ITeacherStorage _lectorStorage;
        private readonly IInterimReportStorage _interimReportStorage;
        private readonly IStudentStorage _studentStorage;
        private readonly IDisciplineStorage _subjectStorage;
        private readonly SaveToWordWarehouser _saveToWordWarehouser;
        public DocReportWarehouserLogic(ITeacherStorage lectorStorage, IInterimReportStorage interimReportStorage, IStudentStorage studentStorage, IDisciplineStorage subjectStorage,
            SaveToWordWarehouser saveToWordWarehouser, SaveToExcelWarehouser saveToExcelWarehouser)
        {
            _lectorStorage = lectorStorage;
            _interimReportStorage = interimReportStorage;
            _studentStorage = studentStorage;
            _subjectStorage = subjectStorage;
            _saveToWordWarehouser = saveToWordWarehouser;
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
                foreach (var subject in student.Disciplines)
                {
                    record.Disciplines.Add(subject.Value);
                }
                list.Add(record);
            }
            return list;
        }
        /*public List<ReportInterimReportViewModel> GetInterimReports(ReportBindingModel model)
        {
            return _interimReportStorage.GetByDiscipline(model.DateFrom, model.DateTo, model.DisciplineId);
        }
        public void SaveTeacherStudentsToWordFile(ReportBindingModel model, List<StudentViewModel> students)
        {
            SaveToWordWarehouser.CreateDoc(new WordInfoWarehouser
            {
                FileName = model.FileName,
                TeacherName = _lectorStorage.GetElement(new TeacherBindingModel
                {
                    Id = model.TeacherId
                }).Name,
                Title = "Список студентов",
                Students = students
            });
        }*/
       /* public void SaveTeacherStudentToExcelFile(ReportBindingModel model, List<StudentViewModel> students)
        {
            SaveToExcelWarehouser.CreateDoc(new ExcelInfoWarehouser
            {
                FileName = model.FileName,
                TeacherName = _lectorStorage.GetElement(new TeacherBindingModel
                {
                    Id = model.TeacherId
                }).Name,
                Title = "Список студентов",
                Students = students
            });
        }*/

        /*[Obsolete]
        public void SaveInterimReportsByDateByDisciplineToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Отчёт по дисциплине",
                InterimReports = GetInterimReports(new ReportBindingModel
                {
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    DisciplineId = model.DisciplineId
                }),
                DisciplineName = _subjectStorage.GetElement(new DisciplineBindingModel
                {
                    Id = model.DisciplineId
                }).Name,
                DateFrom = (DateTime)model.DateFrom,
                DateTo = (DateTime)model.DateTo,
            });
        }*/
    }
}
