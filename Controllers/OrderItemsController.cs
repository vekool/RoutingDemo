using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		public async Task<IActionResult> AddOrderItem(int? orderId)
		{
			if (orderId == null)
			{
				return NotFound();
			}
			Order? o = await ctx.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();

			if (o == null) {
				return NotFound();
			}
			OrderDetailsVM vm = new OrderDetailsVM();
			vm.OrderId = o.Id;
			vm.ProductList = await (from p in ctx.Products
									orderby p.Name
									select new ProductMini
									{
										Id = p.Id,
										Name = p.Name
									}).ToListAsync();
			return View(vm);	
		}

		[HttpPost("AddOrderItem")]
		public async Task<IActionResult> AddOrderItem(OrderDetailsVM? vm)
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
				await ctx.SaveChangesAsync();
				return RedirectToAction("OrderDetailsFor", "Orders", new { oid = vm.OrderId });
			}
			else
			{
				return View(vm);
			}
		}

	}
}
