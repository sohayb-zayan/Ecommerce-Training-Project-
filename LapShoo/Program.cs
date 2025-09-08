using LapShoo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyData;
using MyDataAcc.Implementation;
using MyShop.Entities.Model;
using MyShop.Entities.Repo;

namespace LapShoo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<Useres, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.SlidingExpiration = true;
                options.Cookie.IsEssential = true;
                options.LoginPath = "/Ideintity/AccountManeger/LogIn";
                options.AccessDeniedPath = "/Ideintity/AccountManeger/AccessDenied";
            });

            builder.Services.AddScoped<RedirectService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                await SeedService.SeedDatabase(scope.ServiceProvider);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
                    {
                        if (context.User.IsInRole("Admin"))
                        {
                            context.Response.Redirect("/Admin/Category/Index"); 
                            return;
                        }
                        else if (context.User.IsInRole("User"))
                        {
                            context.Response.Redirect("/Customer/Home/Index"); 
                            return;
                        }
                        else
                        {
                            
                            context.Response.Redirect("/Ideintity/AccountManeger/AccessDenied");
                            return;
                        }
                    }
                    else
                    {
                        context.Response.Redirect("/Ideintity/AccountManeger/LogIn");
                        return;
                    }
                }

                await next();
            });


            app.UseStatusCodePages(context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == 401) // Unauthorized
                {
                    response.Redirect("/Ideintity/AccountManeger/LogIn");
                }
                else if (response.StatusCode == 403) // Forbidden ? 
                {
                    response.Redirect("/Ideintity/AccountManeger/AccessDenied");
                }

                return Task.CompletedTask;
            });

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
