using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfoWarehouser
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string TeacherName { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }
}
