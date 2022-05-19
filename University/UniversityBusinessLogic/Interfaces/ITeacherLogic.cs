using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface ITeacherLogic
    {
        List<TeacherViewModel> Read(TeacherBindingModel model);
        void CreateOrUpdate(TeacherBindingModel model);
        void Delete(TeacherBindingModel model);
    }
}
