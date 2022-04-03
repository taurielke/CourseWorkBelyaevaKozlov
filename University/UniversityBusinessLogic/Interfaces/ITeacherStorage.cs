using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface ITeacherStorage
    {
        List<TeacherViewModel> GetFullList();
        List<TeacherViewModel> GetFilteredList(TeacherBindingModel model);
        TeacherViewModel GetElement(TeacherBindingModel model);
        void Insert(TeacherBindingModel model);
        void Update(TeacherBindingModel model);
        void Delete(TeacherBindingModel model);
    }
}
