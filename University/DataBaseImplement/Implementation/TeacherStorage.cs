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
    public class TeacherStorage : ITeacherStorage
    {
        public List<TeacherViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Teachers
                .Select(rec => new TeacherViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    DisciplineName = context.Disciplines.FirstOrDefault(recDiscipline => rec.DisciplineId == recDiscipline.Id).Name,
                    DisciplineId = rec.DisciplineId
                }).ToList();
            }
        }
        public List<TeacherViewModel> GetFilteredList(TeacherBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Teachers
                .Include(rec => rec.LearningPlanTeachers)
                .ThenInclude(rec => rec.LearningPlan)
                .Where(rec => rec.DisciplineId == model.DisciplineID)
                .Select(rec => new TeacherViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    DisciplineName = context.Disciplines.FirstOrDefault(recDiscipline => rec.DisciplineId == recDiscipline.Id).Name,
                    DisciplineId = rec.DisciplineId
                })
                .ToList();
            }
        }
        public TeacherViewModel GetElement(TeacherBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var teacher = context.Teachers
                .Include(rec => rec.LearningPlanTeachers)
                .ThenInclude(rec => rec.LearningPlan)
                .FirstOrDefault(rec => rec.Name == model.TeacherName || rec.Id == model.Id);
                return teacher != null ?
                new TeacherViewModel
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    DisciplineName = context.Disciplines.FirstOrDefault(recDiscipline => teacher.DisciplineId == recDiscipline.Id).Name,
                    DisciplineId = teacher.DisciplineId
                } :
                null;
            }
        }
        public void Insert(TeacherBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Teachers.Add(CreateModel(model, new Teacher()));
                context.SaveChanges();
            }
        }
        public void Update(TeacherBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.Teachers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(TeacherBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Teacher element = context.Teachers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Teachers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Teacher CreateModel(TeacherBindingModel model, Teacher teacher)
        {
            teacher.Name = model.TeacherName;
            teacher.DisciplineId = model.DisciplineID;
            return teacher;
        }
    }
}
