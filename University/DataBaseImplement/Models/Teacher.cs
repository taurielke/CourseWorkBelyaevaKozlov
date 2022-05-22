using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDataBaseImplement.Models
{
	public class Teacher
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int DisciplineId { get; set; }
		[ForeignKey("TeacherId")]
		public virtual List<LearningPlanTeacher> LearningPlanTeachers { get; set; }
	}
}
