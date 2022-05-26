using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.OfficePackage;
using UniversityBusinessLogic.OfficePackage.HelperModels;
using System.Linq;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly ILearningPlanStorage learningPlanStorage;
        private readonly IStudentStorage studentStorage;
        private readonly IAttestationStorage attestationStorage;
        private readonly IDisciplineStorage disciplineStorage;
        private readonly ITeacherStorage teacherStorage;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToPdf _saveToPdf;


        public ReportLogic(ILearningPlanStorage learningPlanStorage, IStudentStorage studentStorage, 
            IAttestationStorage attestationStorage,IDisciplineStorage disciplineStorage, ITeacherStorage teacherStorage,
            AbstractSaveToWord saveToWord, AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf)
        {
            this.learningPlanStorage = learningPlanStorage;
            this.studentStorage = studentStorage;
            this.attestationStorage = attestationStorage;
            this.disciplineStorage = disciplineStorage;
            this.teacherStorage = teacherStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        public List<ReportLearningPlanDisciplinesViewModel> GetLearningPlanDisciplines(ReportBindingModel model)
        {
            var learningPlans = model.LearningPlans;
            var list = new List<ReportLearningPlanDisciplinesViewModel>();
            foreach (var learningPlan in learningPlans)
            {
                var record = new ReportLearningPlanDisciplinesViewModel
                {
                    LearningPlanName = learningPlan.LearningPlanName,
                    Disciplines = new List<string>()
                };
                foreach (var teacher in learningPlan.LearningPlanTeachers)
                {
                    var teach = teacherStorage.GetElement(new TeacherBindingModel { Id = teacher.Key });
                    var discipline = disciplineStorage.GetElement(new DisciplineBindingModel { Id = teach.DisciplineId });
                    record.Disciplines.Add(discipline.Name);
                }
                /*var deposits = _depositStorage.GetFullList().Where(rec => rec.DepositClients.Keys.ToList().Contains(client.Id)).ToList();
                foreach (var deposit in deposits)
                {
                    var currencies = _currencyStorage.GetFullList().Where(rec => rec.CurrencyDeposits.Keys.ToList().Contains(deposit.Id)).ToList();
                    record.Currencies.AddRange(currencies.Select(cur => cur.CurrencyName));
                }
                record.Currencies = record.Currencies.Distinct().ToList();*/
                list.Add(record);
            }
            return list;
        }

        public List<ReportAttestationsViewModel> GetAttestations(ReportBindingModel model)
        {
            var list = new List<ReportAttestationsViewModel>();
            var attestations = attestationStorage.GetFilteredList(new AttestationBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                DeaneryId = model.DeaneryId
            });
            foreach (var att in attestations)
            {
                var record = new ReportAttestationsViewModel
                {
                    StudentName = studentStorage.GetElement(new StudentBindingModel { GradebookNumber = att.StudentId}).Name,
                    AttestationDate = att.Date,
                    LearningPlanDisciplines = new List<(LearningPlanViewModel, List<DisciplineViewModel>)>()
                };
                var learningPlans = learningPlanStorage.GetFullList().Where(rec => rec.LearningPlanStudents.Keys.ToList().Contains(att.StudentId)).ToList();
                foreach (var lp in learningPlans)
                {
                    var discList = new List<DisciplineViewModel>();
                    foreach (var teacher in lp.LearningPlanTeachers)
                    {
                        var teach = teacherStorage.GetElement(new TeacherBindingModel { Id = teacher.Key });
                        var discipline = disciplineStorage.GetElement(new DisciplineBindingModel { Id = teach.DisciplineId });
                        discList.Add(discipline);
                    }
                    record.LearningPlanDisciplines.Add((lp, discList));
                }
                list.Add(record);
            }
            return list;
        }


        public void SaveLearningPlanDisciplinesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Дисциплины по планам обучения",
                LearningPlanDisciplines = GetLearningPlanDisciplines(model)
            });
        }

        public void SaveLearningPlanDisciplinesToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Дисциплины по планам обучения",
                LearningPlanDisciplines = GetLearningPlanDisciplines(model)
            });
        }

        public void SaveAttestationsToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Сведения по аттестации студентов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Attestations = GetAttestations(model)
            });
        }
    }
}
