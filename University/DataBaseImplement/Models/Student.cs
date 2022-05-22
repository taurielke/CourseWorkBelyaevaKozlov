using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityDataBaseImplement.Models;
using UniversityDataBaseImplementation.Models;

namespace UniversityDataBaseImplement.Models
{
	public class Student
	{
		[Key]
		public string GradebookNumber { get; set; }
		[Required]
		public string Name { get; set; }
		[ForeignKey("GradebookNumber")]
		public virtual List<StudentDiscipline> StudentDisciplines { get; set; }
		[ForeignKey("GradebookNumber")]
		public virtual List<LearningPlanStudent> LearningPlanStudents { get; set; }
	}
}
