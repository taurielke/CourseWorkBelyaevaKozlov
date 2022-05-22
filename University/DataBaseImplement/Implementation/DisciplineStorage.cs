using Microsoft.EntityFrameworkCore;
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
    public class DisciplineStorage : IDisciplineStorage
    {
        public List<DisciplineViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Disciplines
                .Select(rec => new DisciplineViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    DepartmentName = context.Departments.FirstOrDefault(x => x.DepartmentLogin == rec.DepartmentLogin).Name,
                }).ToList();
            }
        }
        public List<DisciplineViewModel> GetFilteredList(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Disciplines
                .Where(rec => rec.DepartmentLogin == model.DepartmentLogin)
                .Select(rec => new DisciplineViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    DepartmentName = context.Departments.FirstOrDefault(x => x.DepartmentLogin == model.DepartmentLogin).Name,
                })
                .ToList();
            }
        }
        public DisciplineViewModel GetElement(DisciplineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var subject = context.Disciplines
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
                return subject != null ?
                new DisciplineViewModel
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    DepartmentName = context.Departments.FirstOrDefault(x => x.DepartmentLogin == subject.DepartmentLogin).Name
                } :
                null;
            }
        }
        public void Insert(DisciplineBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Disciplines.Add(CreateModel(model, new Discipline()));
                context.SaveChanges();
            }
        }
        public void Update(DisciplineBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.Disciplines.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(DisciplineBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
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
        }
        private Discipline CreateModel(DisciplineBindingModel model, Discipline subject)
        {
            subject.Name = model.Name;
            subject.DepartmentLogin = model.DepartmentLogin;
            return subject;
        }
    }
}
