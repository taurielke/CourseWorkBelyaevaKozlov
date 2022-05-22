using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityDataBaseImplementation.Models;

namespace UniversityDataBaseImplement.Models
{
	public class Discipline
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[ForeignKey("DepartmentLogin")]
		public string DepartmentLogin { get; set; }
		[ForeignKey("DisciplineId")]
		public virtual List<StudentDiscipline> StudentDisciplines { get; set; }
	}
}
