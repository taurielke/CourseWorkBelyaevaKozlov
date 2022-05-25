using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfoWarehouser
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string TeacherName { get; set; }
        public List<StudentViewModel> Students { get; set; }
        public IEnumerable<object> Disciplines { get; internal set; }
    }
}
