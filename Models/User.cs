using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoutingDemo.Models
{
	public class User:IdentityUser
	{
		[Display(Name ="Full Name")]
		public string? fullname { get; set; }
		//Idenity framework already adds this
		//public string? email { get; set; }
		[Display(Name ="Permanent Address")]
		[DataType(DataType.MultilineText)]
		public string? address { get; set; }
		[Display(Name ="Age in Years")]
		public float? age { get; set; }
		[Required]
		[MinLength(5)]
		[NotMapped] //identity framework handles password
		[DataType(DataType.Password)]

		public string? Password { get; set; }
		[Required]
		[MinLength(5)]
		[Compare("Password", ErrorMessage ="Passwords do not match")]
		[NotMapped]//identity framework handles password = this is good for registration
		[Display(Name ="Confirm Password")]
		[DataType(DataType.Password)]
		public string? ComparePassword { get; set; }

		[Display(Name ="PIN/ZIP Code")]
		public string? zipcode { get; set; }
	}
}
