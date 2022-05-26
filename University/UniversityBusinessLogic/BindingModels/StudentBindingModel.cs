using System.Collections.Generic;

namespace UniversityBusinessLogic.BindingModels
{
	public class StudentBindingModel
	{
		public int? GradebookNumber { get; set; }
		public string StreamName { get; set; }
		public int? DeaneryId { get; set; }
		public string Name { get; set; }
		public Dictionary<int, string> Disciplines { get; set; }
		public Dictionary<int, string> LearningPlans { get; set; }
	}
}
