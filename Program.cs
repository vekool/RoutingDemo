using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoutingDemo.Models;
using System.Threading.RateLimiting;
using Serilog;


namespace RoutingDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.File(
                        path: "logs/myapp.txt",
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] " +
                        "RequestId={RequestId} {Message:lj}{NewLine}{Exception}"
                    );
            });

			builder.Services.AddDbContext<SampleContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );
            builder.Services.AddMemoryCache();
            _ = builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                {
                    string key;
                    if (context.User.Identity?.IsAuthenticated == true)
                    {
                        key = context.User.Identity.Name;
                    }
                    else
                    {
                        key = context.Connection.RemoteIpAddress?.ToString() ?? "anonymus";
                    }
                    return RateLimitPartition.GetFixedWindowLimiter(key, _ =>
                    {
                        FixedWindowRateLimiterOptions fixedWindowRateLimiterOptions = new()
                        {
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1)
                        };
                        return fixedWindowRateLimiterOptions;
                    });
                });
            });
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

            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                using (Serilog.Context.LogContext.PushProperty("RequestId", context.TraceIdentifier))
                {
                    await next(context);
                }
            });
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            //app supports routing
            app.UseRouting();
            app.UseAuthentication(); //login / logout support
            //app supports authorization (later)
            app.UseAuthorization();

            //allow mapping html / css / js / images and other static files
            app.MapStaticAssets();
            
            //default routing

            app.UseRateLimiter();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            //run the app
            app.Run();
        }
    }
}
