using System;
using System.Collections.Generic;
using System.Text;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplementation.Models
{
    public class StudentDiscipline
    {
        public int Id { get; set; }
        public int GradebookNumber { get; set; }
        public int DisciplineId { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual Student Student { get; set; }
    }
}
