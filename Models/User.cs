using System.ComponentModel.DataAnnotations;

namespace RoutingDemo.Models
{
	public class User
	{
		public string? fullname { get; set; }
		public string? email { get; set; }
		public string? address { get; set; }
		public float? age { get; set; }
		[Required]
		[MinLength(5)]
		public string? Password { get; set; }
		[Required]
		[MinLength(5)]
		[Compare("Password", ErrorMessage ="Passwords do not match")]
		public string? ComparePassword { get; set; }

		public string? zipcode { get; set; }
	}
}
