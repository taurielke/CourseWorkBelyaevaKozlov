using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UniversityDataBaseImplement.Models;

namespace UniversityDataBaseImplement.Models
{
	public class Deanery
	{
		public int Id { get; set; }
		[Required]
		public string Login { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
		[ForeignKey("DeaneryId")]
		public virtual List<LearningPlan> LearningPlans { get; set; }

		[ForeignKey("DeaneryId")]
		public virtual List<Student> Students { get; set; }

		[ForeignKey("DeaneryId")]
		public virtual List<Attestation> Attestations { get; set; }
	}
}
