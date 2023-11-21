using System.ComponentModel.DataAnnotations;

namespace PustokAB202.Models
{
	public class Author
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="Ad mutleqdir")]

		public string Fullname { get; set; }

		public List<Book>? Books { get; set; }
	}
}
