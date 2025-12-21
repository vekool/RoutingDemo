using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoutingDemo.Models;

namespace RoutingDemo
{
    public class Program
    {
        public static void Main(string[] args)
       {
            
            //this is what our website is - a web app - 
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //add controller and view support
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<SampleContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );
            builder.Services.AddIdentity<User, IdentityRole>(
                options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;

                    options.User.RequireUniqueEmail = true;
                    //if user enters incorrect username / password 5 times, the account is locked out / disabled
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                }
            ).AddEntityFrameworkStores<SampleContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.LogoutPath = "/User/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app supports routing
            app.UseRouting();
            app.UseAuthentication(); //login / logout support
            //app supports authorization (later)
            app.UseAuthorization();

            //allow mapping html / css / js / images and other static files
            app.MapStaticAssets();

            //default routing
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            //run the app
            app.Run();
        }
    }
}
