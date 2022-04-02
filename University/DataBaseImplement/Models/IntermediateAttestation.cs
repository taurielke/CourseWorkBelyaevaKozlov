using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
    public class IntermediateAttestation
    {
        public int Id { get; set; }
        public int RecordBookNumber { get; set; }
        public int SemesterNumber { get; set; }
        public int DisciplineId { get; set; }
        public int Mark { get; set; }
        public virtual Student Student { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}
