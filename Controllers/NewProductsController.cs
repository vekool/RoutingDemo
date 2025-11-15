using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;

namespace RoutingDemo.Controllers
{

	
	[Route("NewProducts")]
	public class NewProductsController : Controller
	{
		
		private readonly SampleContext ctx;

		//dependency injection ->
		//you framework autmatically passes the context to this controller
		public NewProductsController(SampleContext c)
		{
			ctx = c;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return View(ctx.Products.ToList());
		}
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
			ctx.Products.Add(p);
			ctx.SaveChanges(); //commit
			return View(new Product());
		}
		[HttpGet("EditProduct")]
		public IActionResult EditProduct(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}
			Product? p = ctx.Products.Where(x => x.Id == id.Value).FirstOrDefault();

			if (p == null)
			{ 
				return NotFound();
			}
			return View(p);
		}
		[HttpGet("DeleteProduct")]
		public IActionResult DeleteProduct(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Product? p = ctx.Products.Where(x => x.Id == id.Value).FirstOrDefault();

			if (p == null)
			{
				return NotFound();
			}
			ctx.Products.Remove(p);
			ctx.SaveChanges();
			return RedirectToAction(nameof(GetAll));
		}
		[HttpPost("EditProduct")]
		public IActionResult EditProduct(Product p)
		{
			if (!ModelState.IsValid) return View(p);

			Product? old = ctx.Products.Where(x => x.Id == p.Id).SingleOrDefault();

			if (old == null)
			{
				return NotFound();
			}
			ctx.Entry(old).CurrentValues.SetValues(p);
			ctx.SaveChanges();
			return RedirectToAction(nameof(GetAll));
		}
			
	}
}
//Migrations
//Step by Step
//Tools -> NPM -> Package Manager Console
