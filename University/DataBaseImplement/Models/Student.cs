using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class Student
    {
        //если у нас primary key - это номер зачетки, то получается без ID будем? + на диаграмме номер зачетки - это int,
        //а у нас в зачетках есть слэш, то есть надо стринг что ли? 
        //или в унике для которого прогу делаем нет слэшей в номерах зачеток тогда
        public int RecordBookNumber { get; set; }
        [Required]
        public string StudentName { get; set; }
        public DateTime EnrollingDate { get; set; }
        [Required]
        public int CourseYear { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        [ForeignKey("RecordBookNumber")]
        public virtual List<StudentLearningPlan> StudentLearningPlans { get; set; }
    }
}
