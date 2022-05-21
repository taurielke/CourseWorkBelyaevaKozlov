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
            .Where(rec => rec.Email == model.Email)
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
            var Deanery = context.Deaneries
            .Include(rec => rec.LearningPlans)
            .FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);
            return Deanery != null ? CreateModel(Deanery) : null;
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
        private static Deanery CreateModel(DeaneryBindingModel model, Deanery Deanery)
        {
            Deanery.DeaneryName = model.DeaneryName;
            Deanery.Email = model.Email;
            Deanery.Password = model.Password;
            return Deanery;
        }
        private static DeaneryViewModel CreateModel(Deanery Deanery)
        {
            return new DeaneryViewModel
            {
                Id = Deanery.Id,
                DeaneryName = Deanery.DeaneryName,
                Email = Deanery.Email,
                Password = Deanery.Password
            };
        }
    }
}
