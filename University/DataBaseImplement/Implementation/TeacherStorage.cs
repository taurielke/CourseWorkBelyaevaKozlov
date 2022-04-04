using System;
using System.Collections.Generic;
using System.Text;
using UniversityDataBaseImplement.Models;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.Interfaces;
using System.Linq;

namespace UniversityDataBaseImplement.Implementation
{
    public class TeacherStorage : ITeacherStorage
    {
        public List<TeacherViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Teachers.Select(CreateModel).ToList();
        }
        public List<TeacherViewModel> GetFilteredList(TeacherBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Teachers.Where(rec => rec.TeacherName.Contains(model.TeacherName)).Select(CreateModel).ToList();
        }

        public TeacherViewModel GetElement(TeacherBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var teacher = context.Teachers.FirstOrDefault(rec => rec.TeacherName == model.TeacherName || rec.Id == model.Id);
            return teacher != null ? CreateModel(teacher) : null;
        }

        public void Insert(TeacherBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Teachers.Add(CreateModel(model, new Teacher()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            
        }
        public void Update(TeacherBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Teachers.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(TeacherBindingModel model)
        {
            using var context = new UniversityDatabase();
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
        private static Teacher CreateModel(TeacherBindingModel model, Teacher teacher)
        {
            teacher.TeacherName = model.TeacherName;
            return teacher;
        }

        private static TeacherViewModel CreateModel(Teacher teacher)
        {
            return new TeacherViewModel
            {
                Id = teacher.Id,
                TeacherName = teacher.TeacherName
            };
        }
    }
}
