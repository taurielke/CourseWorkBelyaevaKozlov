using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Implements
{
    public class LearningPlanStorage : ILearningPlanStorage
    {
        public List<LearningPlanViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.LearningPlans
                .Select(rec => new LearningPlanViewModel
                {
                    Id = rec.Id,
                    StreamName = rec.StreamName,
                    Hours = rec.Hours,
                }).ToList();
            }
        }
        public List<LearningPlanViewModel> GetFilteredList(LearningPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.LearningPlans
                .Where(rec => rec.StreamName == model.StreamName)
                .Select(rec => new LearningPlanViewModel
                {
                    Id = rec.Id,
                    StreamName = rec.StreamName,
                    Hours = rec.Hours,
                })
                .ToList();
            }
        }
        public LearningPlanViewModel GetElement(LearningPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var ep = context.LearningPlans
                .FirstOrDefault(rec => rec.StreamName == model.StreamName || rec.Id == model.Id);
                return ep != null ?
                new LearningPlanViewModel
                {
                    Id = ep.Id,
                    StreamName = ep.StreamName,
                    Hours = ep.Hours
                } :
                null;
            }
        }
        public void Insert(LearningPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.LearningPlans.Add(CreateModel(model, new LearningPlan()));
                context.SaveChanges();
            }
        }
        public void Update(LearningPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.LearningPlans.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(LearningPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
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
        }
        private LearningPlan CreateModel(LearningPlanBindingModel model, LearningPlan LearningPlan)
        {
            LearningPlan.StreamName = model.StreamName;
            LearningPlan.Hours = model.Hours;
            return LearningPlan;
        }
    }
}
