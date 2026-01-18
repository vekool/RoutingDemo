using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RoutingDemo.Models;
using RoutingDemo.Models.ViewModel;

namespace RoutingDemo.Controllers
{
	[Route("Orders")]
	public class OrdersController : Controller
	{
		private readonly SampleContext ctx;
		private readonly IMemoryCache _memoryCache;
		private readonly UserManager<User> _userManager;

		public OrdersController(SampleContext c, IMemoryCache memoryCache, UserManager<User> userManager)
		{
			ctx = c;
			_memoryCache = memoryCache;
			_userManager = userManager;
		}
		[HttpGet("Index")]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			string userId = _userManager.GetUserId(User) ?? "0";

			string cacheKey = $"AllOrders_{userId}";
			if (!_memoryCache.TryGetValue(cacheKey, out List<AllOrdersVM> data))
			{
				data = await (from p in ctx.Products
							join od in ctx.OrderDetails on p.Id equals od.ProductId
							join o in ctx.Orders on od.OrderId equals o.Id
							select new AllOrdersVM
							{
								Name = p.Name,
								ODId = od.Id,
								OID = o.Id,
								OrderDate = o.OrderDate,
								Quantity = od.Quantity,
								Rate = od.Rate
							}).ToListAsync();

				_memoryCache.Set(cacheKey, data, TimeSpan.FromMinutes(5));
			}
			return View(data);
		}
		[HttpGet("{oid?}")]
		public async Task<IActionResult> OrderDetailsFor(int? oid)
		{
			if (oid == null)
			{
				return BadRequest();
			}

			Order? Order = await (from o in ctx.Orders
						   where o.Id == oid.Value
						   select o).FirstOrDefaultAsync();
			if (Order == null) {
				return NotFound();
			}
			OrderEditVM ovm = new OrderEditVM();
			ovm.OrderId = Order.Id;
			ovm.OrderDate = Order.OrderDate;
			ovm.OrderDetailRows =await (from p in ctx.Products
								   join od in ctx.OrderDetails
								   on p.Id equals od.ProductId
								   where od.OrderId == Order.Id
								   select new OrderDetailsWithProduct
								   {
									   ODId = od.Id,
									   ProductName = p.Name,
									   Quantity = od.Quantity,
									   Rate = od.Rate
								   }).ToListAsync();
			
			return View(ovm);
		}
	}
}
