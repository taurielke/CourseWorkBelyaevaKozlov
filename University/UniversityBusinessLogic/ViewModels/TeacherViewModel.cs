using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class TeacherViewModel
	{
		public int Id { get; set; }
		[DisplayName("Имя")]
		public string Name { get; set; }
		[DisplayName("Предмет")]
		public string DisciplineName { get; set; }
		public int DisciplineId { get; set; }
	}
}
