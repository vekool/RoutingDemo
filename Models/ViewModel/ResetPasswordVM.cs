using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoutingDemo.Models.ViewModel
{
	public class ResetPasswordVM
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		public string? Token { get; set; }

		[Required]
		[MinLength(5)]
		[NotMapped]
		[DataType(DataType.Password)]

		public string? Password { get; set; } = "";
		[Required]
		[MinLength(5)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		[NotMapped]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		public string? ComparePassword { get; set; } = "";
	}
}
