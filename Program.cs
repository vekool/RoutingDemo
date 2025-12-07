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
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<SampleContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app supports routing
            app.UseRouting();

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
