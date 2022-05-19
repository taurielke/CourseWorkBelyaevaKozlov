using System;
using System.Collections.Generic;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IStudentStorage _studentStorage;

        public StudentLogic(IStudentStorage studentStorage)
        {
            _studentStorage = studentStorage;
        }

        public List<StudentViewModel> Read(StudentBindingModel model)
        {
            if (model == null)
            {
                return _studentStorage.GetFullList();
            }
            if (model.RecordBookNumber.HasValue)
            {
                return new List<StudentViewModel>
                {
                    _studentStorage.GetElement(model)
                };
            }
            return _studentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel
            {
                StudentName = model.StudentName
            });
            if (element != null && element.RecordBookNumber != model.RecordBookNumber)
            {
                throw new Exception("Такой студент уже существует!");
            }
            if (model.RecordBookNumber.HasValue)
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
            var element = _studentStorage.GetElement(new StudentBindingModel
            {
                RecordBookNumber = model.RecordBookNumber
            });

            if (element == null)
            {
                throw new Exception("Студент не найден");
            }
            _studentStorage.Delete(model);
        }
    }
}
