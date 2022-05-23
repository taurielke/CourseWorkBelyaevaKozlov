using System;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class AttestationViewModel
    {
        public int Id { get; set; }
        [DisplayName("Семестр")]
        public int SemesterNumber { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        public int StudentId { get; set; }

        [DisplayName("Студент")]
        public string StudentName { get; set; }
        public int? DeaneryId { get; set; }
    }
}
