using System.ComponentModel.DataAnnotations;

namespace RoutingDemo.Models.ViewModel
{
	public class OrderEditVM
	{
		public int OrderId { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Order Date")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime OrderDate { get; set; }

		public List<OrderDetailsWithProduct> OrderDetailRows { get; set; } = new List<OrderDetailsWithProduct>();
	}
	public class OrderDetailsWithProduct
	{
		public string ProductName { get; set; } = "";
		public double Quantity { get; set; }

		public double Rate { get; set; }
		public int ODId { get; set; }
	}
}
