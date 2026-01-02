using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoutingDemo.Models;
using RoutingDemo.Models.ViewModel;
namespace RoutingDemo.Controllers
{
	[Route("User")]
	public class UserController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<User> u, SignInManager<User> sm, RoleManager<IdentityRole> r)
		{
			_userManager = u;
			_signInManager = sm;
			_roleManager = r;
		}
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
		[HttpGet("Login")]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,false);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", controllerName: "Orders");
				}
				else if (result.IsLockedOut)
				{
					ModelState.AddModelError(string.Empty, "Your account is locked. Try again later.");
				}
				else if (result.RequiresTwoFactor)
				{
					ModelState.AddModelError(string.Empty, "Two-factor authentication is required.");
				}
				else if (result.IsNotAllowed)
				{
					ModelState.AddModelError(string.Empty, "You must confirm your email before logging in.");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid email or password.");
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
			}
			return View(model);
		}
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

		[HttpGet("Register")]
		public IActionResult Register()
		{
			return View(new User());
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register(User u)
		{
			if (ModelState.IsValid)
			{
				var result = await _userManager.CreateAsync(u, u.Password);
				
				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(u, "StandardUser");
					//Log in the user, (remember me is false)
					//await _signInManager.SignInAsync(u, false);
					//we will change this later
					return RedirectToAction("Index", "Home");
				}
				foreach(var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);	 
				}
				
			}
			return View(u);
		}
		[HttpGet("ForgotPassword")]
		public IActionResult ForgotPassword()
		{
			return View(new ForgotPasswordVM());
		}
		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordVM v)
		{
			if (!ModelState.IsValid)
			{
				return View(v);
			}
			//Find the user
			User u = await _userManager.FindByEmailAsync(v.Email);

			if(u == null)
			{
				return RedirectToAction("Register");
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(u);
			var resetLink = Url.Action("ResetPassword","User", new {email = v.Email, token = token});
			//send email
			//log it here
			return RedirectToAction("ForgotPasswordConfirmation");


		}
		[HttpGet("ResetPassword")]
		public IActionResult ResetPassword(string? token = null, string? email = null)
		{
			if (token == null || email == null)
			{
				return BadRequest("Email or token not provided");
			}
			return View(new ResetPasswordVM() { Email = email, Token = token });
		}
		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
		{
			if (!ModelState.IsValid) return View(vm);

			User u = await _userManager.FindByEmailAsync(vm.Email);
			if(u == null)
			{
				return RedirectToAction("ForgotPassword");
			}
			var result = await _userManager.ResetPasswordAsync(u, vm.Token, vm.Password);

			if (result.Succeeded)
			{
				//log this
				return RedirectToAction("ResetPasswordConfirmation");
			}
			foreach (var e in result.Errors)
			{
				ModelState.AddModelError(string.Empty, e.Description);
			}
			return View(vm);

		}
		[HttpGet("ForgotPaswordConfirmation")]
		public IActionResult ForgotPasswordConfirmation()
		{
			return View();
		}
		[HttpGet("Logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		//ForgotPassword --> Email / username --> Send
		//IF generates a token - and stores it in DB
		//this token can be sent with the link to verify the user
		//thhis token is short lived 10 mins
		//link - Token (encrypted text)
		//link is sent to user in a password reset mail
		//if user clicks the mail - password is reset and new password screen is shown
	}
}
