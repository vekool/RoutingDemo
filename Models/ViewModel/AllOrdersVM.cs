using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace RoutingDemo.Models.ViewModel
{
	public class AllOrdersVM
	{
		[Display(Name ="Order Details Number")]
		public int ODId { get; set; }
		[Display(Name="Order Number")]
		public int OID { get; set; }
		public DateTime OrderDate { get; set; }
		[Display(Name = "Product Name")]
		public string Name { get; set; } = "";
		public double Quantity { get; set; }
		public double Rate { get; set; }
	}
}
/* MVVM -> Model View ViewModel */