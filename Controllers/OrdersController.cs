using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;
using RoutingDemo.Models.ViewModel;

namespace RoutingDemo.Controllers
{
	[Route("Orders")]
	public class OrdersController : Controller
	{
		private readonly SampleContext ctx;

		public OrdersController(SampleContext c)
		{
			ctx = c;
		}
		[HttpGet("Index")]
		[HttpGet]
		public IActionResult Index()
		{
			List<AllOrdersVM> data = (from p in ctx.Products
						join od in ctx.OrderDetails on p.Id equals od.ProductId
						join o in ctx.Orders on od.OrderId equals o.Id
						select new AllOrdersVM
						{
							Name = p.Name,
							ODId = od.Id,
							OID = o.Id,
							OrderDate = o.OrderDate,
							Quantity = od.Quantity,
							Rate= od.Rate
						}).ToList();
			return View(data);
						
		}
	}
}
