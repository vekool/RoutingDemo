using System.ComponentModel.DataAnnotations;

namespace RoutingDemo.Models
{
	public class Product
	{
		//treat this as a database primary key
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Product Name cannot be empty or blank")]
		[StringLength(30, ErrorMessage ="Product name must have more than 1 letter and cannot exceed 30 letters", MinimumLength = 1)]
		[Display(Name="Product Name")]
		public string Name { get; set; } = "";

		[Range(0, 2000000, ErrorMessage ="Price out of range")]
		[Display(Name = "Product Price")]
		[DataType(DataType.Currency)]
		public float Price { get; set; } = 0;

		[Required]
		[DataType(DataType.Date)]
		[Display(Name="Manufacturing Date")]
		[DisplayFormat(DataFormatString ="{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
		public DateTime MDate { get; set; } = DateTime.Now;

		[Required]
		[EmailAddress(ErrorMessage ="Invalid email address")]
		[StringLength(50)]
		[Display(Name="Contact Email")]
		public string? EmailAddress { get; set; }

		[Url(ErrorMessage ="Enter a valid URL")]
		[Display(Name="Official Website")]
		public string? Website { get; set; }
		//WHT-SML-G12-GUCCI
		[RegularExpression("[A-Z]{3}-[A-Z]{3}-[A-Z]{1}[0-9]{2}-[A-Z]{5}", ErrorMessage = "Invalid format. Correct format is something like this: WHT-SML-G12-GUCCI")]
		public string? SKU { get; set; }

		[FileExtensions(ErrorMessage ="Invalid format", Extensions ="jpg, gif, jpeg, webp")]
		[Display(Name="Product Image")]
		public string? Image { get; set; }
	}
}
