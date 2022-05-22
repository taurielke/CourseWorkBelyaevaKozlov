using System;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class InterimReportViewModel
	{
		public int Id { get; set; }
		[DisplayName("Дата экзамена")]
		public DateTime DateOfExam { get; set; }
		[DisplayName("Преподаватель")]
		public string TeacherName { get; set; }
		public int TeacherId { get; set; }
	}
}
