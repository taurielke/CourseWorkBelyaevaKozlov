using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class AttestationViewModel
    {
        public int Id { get; set; }
        public int RecordBookNumber { get; set; }
        [DisplayName("ФИО студента")]
        public string StudentName { get; set; }
        [DisplayName("Номер семестра")]
        public int SemesterNumber { get; set; }
        public int DisciplineId { get; set; }
        [DisplayName("Название дисциплины")]
        public string DisciplineName { get; set; }
        [DisplayName("Оценка")]
        public int Mark { get; set; }
        [DisplayName("Дата проведения экзамена")]
        public DateTime ExamDate { get; set; }
    }
}
