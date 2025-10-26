using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers
{
	[Route("NewProducts")]
	public class NewProductsController : Controller
	{
		[HttpGet("Create")]
		public IActionResult CreateProduct()
		{
			return View(new Product());
		}

		[HttpPost("Create")]
		public IActionResult CreateProduct(Product p)
		{
			if (p == null)
			{
				return View(new Product());
			}
			if (!ModelState.IsValid)
			{
				return View(p);
			}
			//everything ok - now add to database
			return View(new Product());
		}
	}
}
