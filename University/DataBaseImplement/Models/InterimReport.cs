using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityDataBaseImplement.Models
{
	public class InterimReport
	{
		public int Id { get; set; }
		[Required]
		public DateTime DateOfExam { get; set; }
		[Required]
		public int TeacherId { get; set; }
	}
}
