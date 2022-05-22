using System.ComponentModel.DataAnnotations;

namespace UniversityDataBaseImplement.Models
{
	public class Deanery
	{
		[Key]
		public string Login { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
