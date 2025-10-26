using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
	[Route("Calculations")]
	public class CalculationsController : Controller
	{
		[HttpGet("Amount")]
		public IActionResult AmountForm()
		{
			return View();
		}

		[HttpPost("DiscountForm")]
		public IActionResult DiscountForm(double? amt)
		{
			if (amt == null)
			{
				//standard bad request error page
				return BadRequest("Invalid amount or Amount not provided");
			}
			double a = amt.Value;
			double disc = 0;
			if (a >= 2000)
			{
				//15 % disc
				disc = a * 0.15;
			}
			else
			{
				disc = 0;
			}
			double final = a - disc;

			
			ViewBag.Amount = a;
			ViewBag.Disc = disc;
			ViewBag.Final = final;

			return View();

		}
		[HttpGet("Table")]
		public IActionResult TableGen()
		{
			return View();
		}

		[HttpPost("TableGen")]
		public IActionResult TableGen(int? num)
		{
			if (num == null)
			{
				//standard bad request error page
				return BadRequest("Invalid amount or Amount not provided");
			}
			int n = num.Value;
			ViewBag.Num = n;
			//since the names do not match -> you can use this to explicitly open the page
			return View("TablePage");
		}
	}
}
