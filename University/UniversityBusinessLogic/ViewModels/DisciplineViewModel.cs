using System;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class DisciplineViewModel
	{
		public int Id { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
		public string DepartmentName { get; set; }
	}
}
