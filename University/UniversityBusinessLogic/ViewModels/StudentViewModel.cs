using System.Collections.Generic;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class StudentViewModel
	{
		[DisplayName("Номер зачётной книжки")]
		public int GradebookNumber { get; set; }
		public string StreamName { get; set; }	
		[DisplayName("ФИО")]
		public string Name { get; set; }
		public Dictionary<int, string> Disciplines { get; set; }
		public Dictionary<int, string> LearningPlans { get; set; }
		public int? DeaneryId { get; set; }
	}
}
