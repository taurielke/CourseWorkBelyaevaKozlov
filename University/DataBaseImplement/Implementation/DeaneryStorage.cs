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
            using (var context = new UniversityDatabase())
            {
                return context.Deaneries
                .Select(rec => new DeaneryViewModel
                {
                    Login = rec.Login,
                    Name = rec.Name,
                    Password = rec.Password
                }).ToList();
            }
        }
        public List<DeaneryViewModel> GetFilteredList(DeaneryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Deaneries
                .Where(rec => rec.Name == model.Name)
                .Select(rec => new DeaneryViewModel
                {
                    Login = rec.Login,
                    Name = rec.Name,
                    Password = rec.Password
                })
                .ToList();
            }
        }
        public DeaneryViewModel GetElement(DeaneryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var Deanery = context.Deaneries
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Login == model.Login);
                return Deanery != null ?
                new DeaneryViewModel
                {
                    Login = Deanery.Login,
                    Name = Deanery.Name,
                    Password = Deanery.Password,
                } :
                null;
            }
        }
        public void Insert(DeaneryBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Deaneries.Add(CreateModel(model, new Deanery()));
                context.SaveChanges();
            }
        }
        public void Update(DeaneryBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.Deaneries.FirstOrDefault(rec => rec.Login == model.Login);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(DeaneryBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Deanery element = context.Deaneries.FirstOrDefault(rec => rec.Login == model.Login);
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
        }
        private Deanery CreateModel(DeaneryBindingModel model, Deanery Deanery)
        {
            Deanery.Name = model.Name;
            Deanery.Password = model.Password;
            Deanery.Login = model.Login;
            return Deanery;
        }
    }
}
