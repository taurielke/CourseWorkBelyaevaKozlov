using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class TeacherLogic : ITeacherLogic
    {
        private readonly ITeacherStorage _lectorStorage;
        public TeacherLogic(ITeacherStorage lectorStorage)
        {
            _lectorStorage = lectorStorage;
        }
        public List<TeacherViewModel> Read(TeacherBindingModel model)
        {
            if (model == null)
            {
                return _lectorStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TeacherViewModel> { _lectorStorage.GetElement(model) };
            }
            return _lectorStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TeacherBindingModel model)
        {
            var element = _lectorStorage.GetElement(new TeacherBindingModel
            {
                TeacherName = model.TeacherName,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть преподаватель с таким именем");
            }
            if (model.Id.HasValue)
            {
                _lectorStorage.Update(model);
            }
            else
            {
                _lectorStorage.Insert(model);
            }
        }
        public void Delete(TeacherBindingModel model)
        {
            var element = _lectorStorage.GetElement(new TeacherBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _lectorStorage.Delete(model);
        }
    }
}
