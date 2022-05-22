using System;

namespace UniversityBusinessLogic.BindingModels
{
	public class InterimReportBindingModel
	{
		public int? Id { get; set; }
		public DateTime DateOfExam { get; set; }
		public int TeacherId { get; set; }
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
	}
}
