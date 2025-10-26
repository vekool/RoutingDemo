using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;
namespace RoutingDemo.Controllers
{
	[Route("User")]
	public class UserController : Controller
	{
		[HttpGet("Create")]
		public IActionResult Create()
		{
			return View();
		}

		//Imagine a form with 20+ input fileds
		//this would mean a function with 20+ parameters
		//which is bad code, unmanagable
		//[HttpPost("CreateUser")]
		//public IActionResult CreateUser(string fullname, string email, float? age, string address)
		//{

		//}
		//Solution: use a model -> which mimics data
		[HttpPost("CreateUser")]
		public IActionResult CreateUser(User user)
		{
			//Don't use Viewbag / Viewdata
			// You can make spelling mistakes in code (no suggestions from IDE)
			//Ex: ViewBag.Amount = a;
			//ViewBag.Disc = disc;
			//ViewBag.Final = final;
			// Internally a lot of boxing / unboxing / typecasting takes place for this to work, hence it is inefficient

			//Strongly Typed View
			// A view which knows how the data is


			//Do some processing here
			//Add in database, log this etc
			return View(user);

		}
		[HttpGet("Create2")]
		public IActionResult Create2()
		{
			return View();
		}

		[HttpGet("Create3")]
		public IActionResult Create3()
		{
			//strongly typed View 
			//User u = new User();
			//return View(u);
			return View(new User());
		}

		[HttpGet("Create4")]
		public IActionResult Create4()
		{
			//strongly typed View 
			//User u = new User();
			//return View(u);
			return View(new User());
		}
	}
}
