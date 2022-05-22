using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IStudentStorage _studentStorage;
        private readonly ITeacherStorage _lectorStorage;
        public StudentLogic(IStudentStorage studentStorage, ITeacherStorage lectorStorage)
        {
            _studentStorage = studentStorage;
            _lectorStorage = lectorStorage;
        }
        public List<StudentViewModel> Read(StudentBindingModel model)
        {
            if (model == null)
            {
                return _studentStorage.GetFullList();
            }
            if (model.GradebookNumber.HasValue)
            {
                return new List<StudentViewModel> { _studentStorage.GetElement(model) };
            }
            return _studentStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel
            {
                Name = model.Name,
            });
            if (element != null && element.GradebookNumber != model.GradebookNumber)
            {
                throw new Exception("Уже есть студент с таким именем");
            }
            if (model.GradebookNumber.HasValue)
            {
                _studentStorage.Update(model);
            }
            else
            {
                _studentStorage.Insert(model);
            }
        }
        public void Delete(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel { GradebookNumber = model.GradebookNumber });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _studentStorage.Delete(model);
        }

        public List<StudentViewModel> SelectByTeacher(TeacherBindingModel model)
        {
            var lector = _lectorStorage.GetElement(model);
            if (lector == null)
            {
                throw new Exception("Преподаватель не найден");
            }
            return _studentStorage.GetByDisciplineId(lector.DisciplineId);
        }

        public void BindingDiscipline(int gradebookNumber, int subjectId)
        {
            _studentStorage.BindingDiscipline(gradebookNumber, subjectId);
        }
    }
}
