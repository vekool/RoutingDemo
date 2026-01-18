using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
	public class ErrorController : Controller
	{
		private readonly ILogger<ErrorController> _logger;

		public ErrorController(ILogger<ErrorController> logger)
		{
			_logger = logger;
		}
		[Route("Error")]
		public IActionResult Error()
		{
			var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
			var requestId = HttpContext.TraceIdentifier;

			if (ex?.Error != null)
			{
				_logger.LogError(ex.Error, "Unhandled Exception");
			}
			return View(
				new ErrorViewModel
				{
					RequestId = requestId
				}
			);
		}
		public class ErrorViewModel
		{
			public string RequestId { get; set; } = "";
		}
	}
}
