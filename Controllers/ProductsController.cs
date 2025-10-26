using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
	[Route("Products")]
	public class ProductsController : Controller
	{
		[HttpGet]
		[HttpGet("Index")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("Create")]
		[HttpGet("Create/{id:int}/{name}/{price:float}")]
		public IActionResult Create(int? id = 0, string? name ="", float? price = 0)
		{
			//How will i send this data to my page?
			//Ans: ViewBag - to send data from controller to view
		
			ViewBag.Id = id;
			ViewBag.Name = name; 
			ViewBag.Price = price;
			return View();
		}
		[HttpGet("Create2")]
		[HttpGet("Create2/{id:int}/{name}")]
		public IActionResult Create2(int? id = 0, string? name = "")
		{
			//How will i send this data to my page?
			//Ans: ViewData - to send data from controller to view
			//ViewBag is just Viewdata with better syntax

			ViewData["Id"] = id;
			ViewData["Name"] = name;
			
			return View();
		}


	}
}
