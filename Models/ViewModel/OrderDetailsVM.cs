using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoutingDemo.Models.ViewModel
{
	public class OrderDetailsVM
	{
		[Required]
		public int OrderId { get; set; }

		[DisplayName("Select Product")]
		public List<ProductMini>? ProductList { get; set; }

		[Required]
		[Range(0, 1000000)]
		public double Quantity { get; set; }

		[Required]
		[Range(0, 100000000000)]
		public double Rate { get; set; }

		public int SelectedProductId { get; set; }
	}
	public class ProductMini {
		public int Id { get; set; }
		public string Name { get; set; } = "";
	}

}
