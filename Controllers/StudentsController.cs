using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
	//all the methods inside this controller will be accessible via /Students
	[Route("Students")]
	public class StudentsController : Controller
	{
		[HttpGet]
		// /Students
		[HttpGet("Index")]
		// /Students/Index
		//Make your routes specific, not generic
		public string Index()
		{
			return "Students Controler (Index)";
		}
		[HttpGet("{id:int}")]
		[Route("Details/{id:int}")]
		//[HttpGet("{id}")] is the same as above except, this will work only for get requests -> Students/5
		//to access this method, the url will be /Students/Details/1
		public string Details(int? id)
		{
			if(id == null)
			{
				return "Students Controler (Details) with no id";
			}
			return $"Students Controler (Details) with id = {id}";
		}
		[HttpGet("Profile/{name?}")]
		//For querystring to work
		[HttpGet("Profile")]
		//? means it is optional
		public string Profile(string? name = null)
		{
			if (name == null)
			{
				return "Students Controler (Profile) with no name";
			}
			return $"Students Controler (Details) with name = {name}";
		}
		/* The key point is that in ASP.NET Core, action methods need explicit routing attributes if they don't match the conventional routing pattern, especially when they have parameters that need to be mapped from the URL. */

		//Validation
		//id must be an integer, and cannot be less than 1
		[HttpGet("GetUser/{id:int:min(1)}")]
		public string GetUserById(int id)
		{
			return $"User ID: {id}";
		}

		[HttpGet("RandomGUID")]
		public string GetRandomGUID()
		{
			return $"Random GUID: {Guid.NewGuid()}";
		}



		
		// / students/getuser

	}
}
