using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;
using UniversityBusinessLogic.BindingModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IStudentLogic
    {
        List<StudentViewModel> Read(StudentBindingModel model);
        void CreateOrUpdate(StudentBindingModel model);
        void Delete(StudentBindingModel model);
    }
}
