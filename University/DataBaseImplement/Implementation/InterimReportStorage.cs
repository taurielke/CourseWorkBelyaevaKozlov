using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Implements
{
    public class InterimReportStorage : IInterimReportStorage
    {
        public List<InterimReportViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.InterimReports
                .Select(rec => new InterimReportViewModel
                {
                    Id = rec.Id,
                    DateOfExam = rec.DateOfExam,
                    TeacherName = context.Teachers.FirstOrDefault(recTeacher => recTeacher.Id == rec.TeacherId).Name,
                    TeacherId = rec.TeacherId,
                }).ToList();
            }
        }
        public List<InterimReportViewModel> GetFilteredList(InterimReportBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.InterimReports
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.TeacherId == model.TeacherId || rec.DateOfExam == model.DateOfExam) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && (rec.TeacherId == model.TeacherId
                || rec.DateOfExam.Date >= model.DateFrom.Value.Date && rec.DateOfExam.Date <= model.DateTo.Value.Date)))
                .ToList()
                .Select(rec => new InterimReportViewModel
                {
                    Id = rec.Id,
                    DateOfExam = rec.DateOfExam,
                    TeacherName = context.Teachers.FirstOrDefault(recTeacher => recTeacher.Id == rec.TeacherId).Name,
                    TeacherId = rec.TeacherId,
                })
                .ToList();
            }
        }
        public InterimReportViewModel GetElement(InterimReportBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var checkList = context.InterimReports
                .FirstOrDefault(rec => (rec.TeacherId == model.TeacherId && rec.DateOfExam == model.DateOfExam) || rec.Id == model.Id);
                return checkList != null ?
                new InterimReportViewModel
                {
                    Id = checkList.Id,
                    DateOfExam = checkList.DateOfExam,
                    TeacherName = context.Teachers.FirstOrDefault(recTeacher => recTeacher.Id == checkList.TeacherId).Name,
                    TeacherId = checkList.TeacherId,
                } :
                null;
            }
        }
        public void Insert(InterimReportBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.InterimReports.Add(CreateModel(model, new InterimReport()));
                context.SaveChanges();
            }
        }
        public void Update(InterimReportBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.InterimReports.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(InterimReportBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
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
        }
        private InterimReport CreateModel(InterimReportBindingModel model, InterimReport checkList)
        {
            checkList.DateOfExam = model.DateOfExam;
            checkList.TeacherId = model.TeacherId;
            return checkList;
        }

        public List<ReportInterimReportViewModel> GetByDiscipline(DateTime? dateFrom, DateTime? dateTo, int? disciplineId)
        {
            if (dateFrom.HasValue && dateTo.HasValue && disciplineId.HasValue)
            {
                using (var context = new UniversityDatabase())
                {
                    return context.InterimReports
                    .Where(rec => rec.DateOfExam >= dateFrom && rec.DateOfExam <= dateTo &&
                    context.Teachers.FirstOrDefault(l => l.Id == rec.TeacherId && l.DisciplineId == disciplineId) != null)
                    .ToList()
                    .Select(rec => new ReportInterimReportViewModel
                    {
                        InterimReportId = rec.Id,
                        InterimReportDate = rec.DateOfExam,
                        TeacherName = context.Teachers.FirstOrDefault(recTeacher => recTeacher.Id == rec.TeacherId).Name,
                    })
                    .ToList();
                }
            }
            else
            {
                throw new Exception("Данные не переданы");
            }
        }

        public List<InterimReportViewModel> GetByDateRange(InterimReportBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.InterimReports
                .Where(rec => rec.DateOfExam.Date >= model.DateFrom.Value.Date && rec.DateOfExam.Date <= model.DateTo.Value.Date)
                .ToList()
                .Select(rec => new InterimReportViewModel
                {
                    Id = rec.Id,
                    DateOfExam = rec.DateOfExam,
                    TeacherName = context.Teachers.FirstOrDefault(recTeacher => recTeacher.Id == rec.TeacherId).Name,
                    TeacherId = rec.TeacherId,
                })
                .ToList();
            }
        }

        //public List<StatsViewModel> GetByDateRangeWithSubjets(InterimReportBindingModel model)
        //{
        //    using (var context = new UniversityDatabase())
        //    {
        //        return context.InterimReports
        //        .Where(rec => rec.DateOfExam >= model.DateFrom && rec.DateOfExam <= model.DateTo)
        //        .ToList()
        //        .Select(rec => new StatsViewModel
        //        {
        //            InterimReportId = rec.Id,
        //            InterimReportDate = rec.DateOfExam,
        //            ItemName = context.Disciplines.FirstOrDefault(recS => recS.Id == context.Teachers.FirstOrDefault(recTeacher => recTeacher.Id == rec.TeacherId).DisciplineId).Name,
        //        })
        //        .ToList();
        //    }
        //}
    }
}
