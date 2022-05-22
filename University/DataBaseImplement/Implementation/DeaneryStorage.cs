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
    public class DeaneryStorage : IDeaneryStorage
    {
        public List<DeaneryViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Deaneries
                .Select(CreateModel)
                .ToList();
        }
        public List<DeaneryViewModel> GetFilteredList(DeaneryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Deaneries
            .Include(rec => rec.LearningPlans)
            .Include(rec => rec.Attestations)
            .Include(rec => rec.Students)
            .Where(rec => rec.Login == model.Login && rec.Password == model.Password)
            .Select(CreateModel)
            .ToList();
        }
        public DeaneryViewModel GetElement(DeaneryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var deanery = context.Deaneries
            .Include(rec => rec.LearningPlans)
            .FirstOrDefault(rec => rec.Login == model.Login || rec.Id == model.Id);
            return deanery != null ? CreateModel(deanery) : null;
        }
        public void Insert(DeaneryBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Deaneries.Add(CreateModel(model, new Deanery()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(DeaneryBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Deaneries.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(DeaneryBindingModel model)
        {
            using var context = new UniversityDatabase();
            Deanery element = context.Deaneries.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Deaneries.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Deanery CreateModel(DeaneryBindingModel model, Deanery deanery)
        {
            deanery.Name = model.Name;
            deanery.Login = model.Login;
            deanery.Password = model.Password;
            return deanery;
        }
        private static DeaneryViewModel CreateModel(Deanery deanery)
        {
            return new DeaneryViewModel
            {
                Id = deanery.Id,
                Name = deanery.Name,
                Login = deanery.Login,
                Password = deanery.Password
            };
        }
    }
}
