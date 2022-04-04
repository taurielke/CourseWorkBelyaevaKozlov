using System;
using System.Collections.Generic;
using System.Text;
using UniversityDataBaseImplement.Models;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UniversityDataBaseImplement.Implementation
{
    public class InterimReportStorage : IInterimReportStorage
    {
        public List<InterimReportViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.InterimReports.Select(CreateModel).ToList();
        }
        //выводятся промежуточные ведомости за определенный период по студенту и дициплине
        public List<InterimReportViewModel> GetFilteredList(InterimReportBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.InterimReports
                .Where(rec => rec.RecordBookNumber == model.RecordBookNumber
                && rec.DisciplineId == model.DisciplineId
                &&rec.DateCreate>=model.DateFrom && rec.DateCreate <= model.DateTo)
                .Select(CreateModel).ToList();
        }
        public InterimReportViewModel GetElement(InterimReportBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var interimReport = context.InterimReports.FirstOrDefault(rec => rec.RecordBookNumber == model.RecordBookNumber || rec.Id == model.Id);
            return interimReport != null ? CreateModel(interimReport) : null;
        }
        public void Insert(InterimReportBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.InterimReports.Add(CreateModel(model, new InterimReport()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            
        }
        public void Update(InterimReportBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.InterimReports.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            
        }
        public void Delete(InterimReportBindingModel model)
        {
            using var context = new UniversityDatabase();
            InterimReport element = context.InterimReports.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.InterimReports.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static InterimReport CreateModel(InterimReportBindingModel model, InterimReport interimReport)
        {
            interimReport.RecordBookNumber = model.RecordBookNumber;
            interimReport.SemesterNumber = model.SemesterNumber;
            interimReport.DisciplineId = model.DisciplineId;
            interimReport.Mark = model.Mark;
            interimReport.DateCreate = model.DateCreate;
            return interimReport;
        }

        private static InterimReportViewModel CreateModel(InterimReport interimReport)
        {
            using var context = new UniversityDatabase();
            int? TeacherId = context.Disciplines.FirstOrDefault(rec => rec.Id == interimReport.DisciplineId)?.TeacherId;
            return new InterimReportViewModel
            {
                Id = interimReport.Id,
                RecordBookNumber = interimReport.RecordBookNumber,
                StudentName = context.Students.FirstOrDefault(rec => rec.RecordBookNumber == interimReport.RecordBookNumber)?.StudentName,
                SemesterNumber = interimReport.SemesterNumber,
                DisciplineId = interimReport.DisciplineId,
                DisciplineName = context.Disciplines.FirstOrDefault(rec => rec.Id == interimReport.DisciplineId)?.DisciplineName,
                TeacherName = context.Teachers.FirstOrDefault(rec => rec.Id == TeacherId)?.TeacherName,
                Mark = interimReport.Mark,
                DateCreate = interimReport.DateCreate
            };
        }
    }
}
