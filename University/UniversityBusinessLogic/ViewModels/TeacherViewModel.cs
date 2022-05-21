using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО преподавателя")]
        public string TeacherName { get; set; }
        [DisplayName("Дисциалина")]
        public string Discipline { get; set; }
        public int DisciplineID { get; set; }
    }
}
