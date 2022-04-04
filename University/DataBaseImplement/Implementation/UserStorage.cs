using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDataBaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityDataBaseImplement.Implementation
{
    public class UserStorage : IUserStorage
    {
        public List<UserViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Users.Select(CreateModel).ToList();
        }
        public List<UserViewModel> GetFilteredList(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Users.Where(rec => rec.Login.Contains(model.Login)).Select(CreateModel).ToList();
        }
        public UserViewModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var user = context.Users.FirstOrDefault(rec => rec.Login == model.Login || rec.Id == model.Id);
            return user != null ? CreateModel(user) : null;
        }

        public void Insert(UserBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Users.Add(CreateModel(model, new User()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

        }
        public void Update(UserBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(UserBindingModel model)
        {
            using var context = new UniversityDatabase();
            User element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Users.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static User CreateModel(UserBindingModel model, User user)
        {
            user.Login = model.Login;
            user.Password = model.Password;
            return user;
        }

        private static UserViewModel CreateModel(User user)
        {
            return new UserViewModel
            {
                Login = user.Login,
                Password = user.Password
            };
        }
    }
}
