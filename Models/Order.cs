using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoutingDemo.Models
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]

		[DataType(DataType.Date)]
		[Display(Name = "Order Date")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
		public DateTime OrderDate { get; set; }

		[Range(0,100000000, ErrorMessage = "Price out of range")]
		[Display(Name = "Order Amount")]
		[DataType(DataType.Currency)]
		public float OrderAmount { get; set; } = 0;
	}
}
