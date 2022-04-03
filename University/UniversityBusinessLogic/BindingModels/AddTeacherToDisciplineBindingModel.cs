using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BindingModels
{
    public class AddTeacherToDisciplineBindingModel
    {
        public int DisciplineId { get; set; }
        public TeacherViewModel Teacher { get; set; }
    }
}
