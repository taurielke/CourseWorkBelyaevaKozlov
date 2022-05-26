using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Models
{
	public class LearningPlan
	{
		public int Id { get; set; }
		public int DeaneryId { get; set; }
		[Required]
		public string LearningPlanName { get; set; }
		[Required]
		public int Hours { get; set; }
		[ForeignKey("LearningPlanId")]
		public virtual List<LearningPlanStudent> LearningPlanStudents { get; set; }
		[ForeignKey("LearningPlanId")]
		public virtual List<LearningPlanTeacher> LearningPlanTeachers { get; set; }
		public virtual Deanery Deanery { get; set; }
	}
}
