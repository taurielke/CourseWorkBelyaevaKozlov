using System;
using System.Collections.Generic;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class TeacherLogic : ITeacherLogic
    {
        private readonly ITeacherStorage _teacherStorage;

        public TeacherLogic(ITeacherStorage teacherStorage)
        {
            _teacherStorage = teacherStorage;
        }

        public List<TeacherViewModel> Read(TeacherBindingModel model)
        {
            if (model == null)
            {
                return _teacherStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TeacherViewModel>
                {
                    _teacherStorage.GetElement(model)
                };
            }
            return _teacherStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(TeacherBindingModel model)
        {
            var element = _teacherStorage.GetElement(new TeacherBindingModel
            {
                TeacherName = model.TeacherName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Такой преподаватель уже существует!");
            }
            if (model.Id.HasValue)
            {
                _teacherStorage.Update(model);
            }
            else
            {
                _teacherStorage.Insert(model);
            }
        }

        public void Delete(TeacherBindingModel model)
        {
            var element = _teacherStorage.GetElement(new TeacherBindingModel
            {
                Id = model.Id
            });

            if (element == null)
            {
                throw new Exception("Преподаватель не найден");
            }
            _teacherStorage.Delete(model);
        }
    }
}
