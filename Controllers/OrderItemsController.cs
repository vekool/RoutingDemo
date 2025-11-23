using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;
using RoutingDemo.Models.ViewModel;

namespace RoutingDemo.Controllers
{
	[Route("OrderDetails")]
	public class OrderItemsController : Controller
	{

		private readonly SampleContext ctx;
		public OrderItemsController(SampleContext c)
		{
			ctx = c;
		}
		[HttpGet("AddOrderItem")]
		public IActionResult AddOrderItem(int? orderId)
		{
			if (orderId == null)
			{
				return NotFound();
			}
			Order? o = ctx.Orders.Where(x => x.Id == orderId).FirstOrDefault();

			if (o == null) {
				return NotFound();
			}
			OrderDetailsVM vm = new OrderDetailsVM();
			vm.OrderId = o.Id;
			vm.ProductList = (from p in ctx.Products
							  orderby p.Name
							  select new ProductMini
							  {
								  Id = p.Id,
								  Name = p.Name
							  }).ToList();
			return View(vm);	
		}

		[HttpPost("AddOrderItem")]
		public IActionResult AddOrderItem(OrderDetailsVM? vm)
		{
			if(vm == null)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				OrderDetails oe = new OrderDetails();
				oe.OrderId = vm.OrderId;
				oe.ProductId = vm.SelectedProductId;
				oe.Rate = vm.Rate;
				oe.Quantity = vm.Quantity;

				ctx.OrderDetails.Add(oe);
				ctx.SaveChanges();
				return RedirectToAction("OrderDetailsFor", "Orders", new { oid = vm.OrderId });
			}
			else
			{
				return View(vm);
			}
		}

	}
}
