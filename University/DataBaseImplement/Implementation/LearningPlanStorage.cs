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
    public class LearningPlanStorage : ILearningPlanStorage
    {
        public List<LearningPlanViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.LearningPlans
            .Include(rec => rec.DisciplineLearningPlans)
            .ThenInclude(rec => rec.Discipline)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        //report with students connected to a specific learningPlan
        public List<LearningPlanViewModel> GetFilteredList(LearningPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.LearningPlans.Include(rec => rec.Students)
                .Where(rec => rec.Id.Equals(model.Id))
                .ToList()
                .Select(CreateModel).ToList();
        }
        public LearningPlanViewModel GetElement(LearningPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var learningPlan = context.LearningPlans
                .Include(rec => rec.DisciplineLearningPlans)
                .ThenInclude(rec => rec.Discipline)
                .FirstOrDefault(rec => rec.LearningPlanName == model.LearningPlanName || rec.Id == model.Id);
            return learningPlan != null ? CreateModel(learningPlan) : null;
        }
        public void Insert(LearningPlanBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                LearningPlan learningPlan = new LearningPlan()
                {
                    LearningPlanName = model.LearningPlanName,
                    SpecialtyName = model.SpecialtyName
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
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.LearningPlans.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(LearningPlanBindingModel model)
        {
            using var context = new UniversityDatabase();
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
            learningPlan.SpecialtyName = model.SpecialtyName;
            if (model.Id.HasValue)
            {
                var LearningPlanLearningPlans = context.DisciplineLearningPlans.Where(rec => rec.Id == model.Id.Value).ToList();
                context.DisciplineLearningPlans.RemoveRange(LearningPlanLearningPlans.Where(rec => !model.DisciplineLearningPlans.ContainsKey(rec.LearningPlanId)).ToList());
                context.SaveChanges();
                foreach (var updateLearningPlan in LearningPlanLearningPlans)
                {
                    model.DisciplineLearningPlans.Remove(updateLearningPlan.LearningPlanId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.DisciplineLearningPlans)
            {
                context.DisciplineLearningPlans.Add(new DisciplineLearningPlan
                {
                    Id = learningPlan.Id,
                    LearningPlanId = pc.Key
                });
                context.SaveChanges();
            }
            return learningPlan;
        }
        private static LearningPlanViewModel CreateModel(LearningPlan learningPlan)
        {
            using var context = new UniversityDatabase();
            return new LearningPlanViewModel
            {
                Id = learningPlan.Id,
                LearningPlanName = learningPlan.LearningPlanName,
                SpecialtyName = learningPlan.SpecialtyName,
                DisciplineLearningPlans = learningPlan.DisciplineLearningPlans.ToDictionary(recPC => recPC.LearningPlanId, recPC => (recPC.LearningPlan?.LearningPlanName))
            };
        }
    }
}
