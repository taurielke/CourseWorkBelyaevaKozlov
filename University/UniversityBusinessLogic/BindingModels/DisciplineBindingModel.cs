using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class DisciplineBindingModel
    {
        public int? Id { get; set; }
        public string DisciplineName { get; set; }
        public string DisciplineDescription { get; set; }
        public string DepartmentLogin { get; set; }
    }
}
