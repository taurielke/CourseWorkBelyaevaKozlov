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
    public class DisciplineStorage : IDisciplineStorage
    {
        public List<DisciplineViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Disciplines
            .Include(rec => rec.DisciplineLearningPlans)
            .ThenInclude(rec => rec.Discipline)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<DisciplineViewModel> GetFilteredList(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Disciplines.Include(rec => rec.Teacher)
               .Where(rec => rec.Id.Equals(model.Id))
               .ToList()
               .Select(CreateModel).ToList();
        }
        public DisciplineViewModel GetElement(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var discipline = context.Disciplines
                .Include(rec => rec.DisciplineLearningPlans)
                .ThenInclude(rec => rec.Discipline)
                .FirstOrDefault(rec => rec.DisciplineName == model.DisciplineName || rec.Id == model.Id);
            return discipline != null ? CreateModel(discipline) : null;
        }
        public void Insert(DisciplineBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Discipline discipline = new Discipline()
                {
                    DisciplineName = model.DisciplineName,
                    DisciplineDescription = model.DisciplineDescription
                };
                context.Disciplines.Add(discipline);
                context.SaveChanges();
                CreateModel(model, discipline, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(DisciplineBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Disciplines.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(DisciplineBindingModel model)
        {
            using var context = new UniversityDatabase();
            Discipline element = context.Disciplines.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Disciplines.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Discipline CreateModel(DisciplineBindingModel model, Discipline discipline, UniversityDatabase context)
        {
            discipline.DisciplineName = model.DisciplineName;
            discipline.DisciplineDescription = model.DisciplineDescription;
            if (model.Id.HasValue)
            {
                var DisciplineDisciplines = context.DisciplineLearningPlans.Where(rec => rec.Id == model.Id.Value).ToList();
                context.DisciplineLearningPlans.RemoveRange(DisciplineDisciplines.Where(rec => !model.DisciplineLearningPlans.ContainsKey(rec.DisciplineId)).ToList());
                context.SaveChanges();
                foreach (var updateDiscipline in DisciplineDisciplines)
                {
                    model.DisciplineLearningPlans.Remove(updateDiscipline.DisciplineId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.DisciplineLearningPlans)
            {
                context.DisciplineLearningPlans.Add(new DisciplineLearningPlan
                {
                    Id = discipline.Id,
                    DisciplineId = pc.Key
                });
                context.SaveChanges();
            }
            return discipline;
        }
        private static DisciplineViewModel CreateModel(Discipline discipline)
        {
            using var context = new UniversityDatabase();
            return new DisciplineViewModel
            {
                Id = discipline.Id,
                DisciplineName = discipline.DisciplineName,
                DisciplineDescription = discipline.DisciplineDescription,
                DisciplineLearningPlans = discipline.DisciplineLearningPlans.ToDictionary(recPC => recPC.DisciplineId, recPC => (recPC.Discipline?.DisciplineName))
            };
        }
    }
}
