using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityDataBaseImplement.Implements
{
    public class LearningPlanStorage : ILearningPlanStorage
    {
        public List<LearningPlanViewModel> GetFullList()
        {
            var context = new UniversityDatabase();
            return context.LearningPlans
                .Include(rec => rec.LearningPlanTeachers)
                .ThenInclude(rec => rec.Teacher)
                .Include(rec => rec.LearningPlanStudents)
                .ThenInclude(rec => rec.Student)
                .Select(CreateModel).ToList();
            
        }
        public List<LearningPlanViewModel> GetFilteredList(LearningPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new UniversityDatabase();   
            return context.LearningPlans
                .Include(rec => rec.LearningPlanTeachers)
                .ThenInclude(rec => rec.Teacher)
                .Include(rec => rec.LearningPlanStudents)
                .ThenInclude(rec => rec.Student)
                .Where(rec => (rec.LearningPlanName == model.LearningPlanName) || (model.DeaneryId.HasValue && rec.DeaneryId == model.DeaneryId))
                .Select(CreateModel)
                .ToList();
            
        }
        public LearningPlanViewModel GetElement(LearningPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new UniversityDatabase();
            
            var lp = context.LearningPlans
                .Include(rec => rec.LearningPlanTeachers)
                .ThenInclude(rec => rec.Teacher)
                .Include(rec => rec.LearningPlanStudents)
                .ThenInclude(rec => rec.Student)
                .FirstOrDefault(rec => rec.LearningPlanName == model.LearningPlanName || rec.Id == model.Id);
            return lp != null ? CreateModel(lp) : null;
            
        }

        public void Insert(LearningPlanBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            model.Students = new Dictionary<int, string>();
            try
            {
                LearningPlan learningPlan = new LearningPlan()
                {
                    LearningPlanName = model.LearningPlanName,
                    Hours = model.Hours,    
                    DeaneryId = (int)model.DeaneryId,   
                };
                context.LearningPlans.Add(learningPlan);
                context.SaveChanges();
                CreateModel(model, learningPlan, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(LearningPlanBindingModel model)
        {
            var context = new UniversityDatabase();
            var element = context.LearningPlans.FirstOrDefault(rec => rec.Id == model.Id);
            if (model.Students == null)
            {
                model.Students = new Dictionary<int, string>();
            }
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element, context);
            context.SaveChanges();
            
        }
        public void Delete(LearningPlanBindingModel model)
        {
            var context = new UniversityDatabase();
            
                LearningPlan element = context.LearningPlans.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.LearningPlans.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            
        }
        private static LearningPlan CreateModel(LearningPlanBindingModel model, LearningPlan learningPlan, UniversityDatabase context)
        {
            learningPlan.LearningPlanName = model.LearningPlanName;
            learningPlan.Hours = model.Hours;
            learningPlan.DeaneryId = (int)model.DeaneryId;
            
            if (model.Id.HasValue)
            {
                var learningPlanTeachers = context.LearningPlanTeachers.Where(rec => rec.LearningPlanId == model.Id.Value).ToList();
                context.LearningPlanTeachers.RemoveRange(learningPlanTeachers);
                var learningPlanStudents = context.LearningPlanStudents.Where(rec => rec.LearningPlanId == model.Id.Value).ToList();
                context.LearningPlanStudents.RemoveRange(learningPlanStudents);
                context.SaveChanges();
            }
            // добавили новые
            foreach (var t in model.Teachers)
            {
                context.LearningPlanTeachers.Add(new LearningPlanTeacher
                {
                    LearningPlanId = learningPlan.Id,
                    TeacherId = t.Key
                });
                context.SaveChanges();
            }
            foreach (var s in model.Students)
            {
                context.LearningPlanStudents.Add(new LearningPlanStudent
                {
                    LearningPlanId = learningPlan.Id,
                    GradebookNumber = s.Key
                });
                context.SaveChanges();
            }
            return learningPlan;
        }

        private static LearningPlanViewModel CreateModel(LearningPlan learningPlan)
        {
            return new LearningPlanViewModel
            {
                Id = learningPlan.Id,
                LearningPlanName = learningPlan.LearningPlanName,
                Hours = learningPlan.Hours,
                LearningPlanTeachers = learningPlan.LearningPlanTeachers
                .ToDictionary(recCLP => recCLP.TeacherId, recCLP => (recCLP.Teacher?.Name)),
                LearningPlanStudents = learningPlan.LearningPlanStudents
                .ToDictionary(recCLP => recCLP.GradebookNumber, recCLP => (recCLP.Student?.Name))
            };
        }
    }
}
