using Blazored.SessionStorage;
using RecipeManagementApp.Interfaces;
using RecipeManagementApp.Services;

namespace RecipeManagementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Get connection string from configuration
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Registering the RecipeService with the connection string
            builder.Services.AddSingleton<RecipeService>(sp => new RecipeService(connectionString));
            builder.Services.AddSingleton<IUserService, UserService>(sp => new UserService(connectionString));

            builder.Services.AddHttpContextAccessor(); // For accessing HttpContext

            // Add services
            builder.Services.AddScoped<SessionService>();
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddScoped<IContactService, ContactService>(sp => new ContactService(connectionString));
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddTransient<CartService>(sp => new CartService(connectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Redirect root URL to /home
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/home");
                return Task.CompletedTask;
            });

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
