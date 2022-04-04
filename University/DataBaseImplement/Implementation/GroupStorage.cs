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
    public class GroupStorage : IGroupStorage
    {
        public List<GroupViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Groups.Select(CreateModel).ToList();
        }
        public List<GroupViewModel> GetFilteredList(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Groups.Where(rec => rec.GroupName.Contains(model.GroupName)).Select(CreateModel).ToList();
        }

        public GroupViewModel GetElement(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var group = context.Groups.FirstOrDefault(rec => rec.GroupName == model.GroupName || rec.Id == model.Id);
            return group != null ? CreateModel(group) : null;
        }

        public void Insert(GroupBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Groups.Add(CreateModel(model, new Group()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            
        }
        public void Update(GroupBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(GroupBindingModel model)
        {
            using var context = new UniversityDatabase();
            Group element = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Groups.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Group CreateModel(GroupBindingModel model, Group group)
        {
            group.GroupName = model.GroupName;
            return group;
        }

        private static GroupViewModel CreateModel(Group group)
        {
            return new GroupViewModel
            {
                Id = group.Id,
                GroupName = group.GroupName
            };
        }
    }
}
