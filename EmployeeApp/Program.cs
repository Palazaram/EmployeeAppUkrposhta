using EmployeeApp.Models;
using EmployeeApp.Repositories.CompanyRepository;
using EmployeeApp.Repositories.DepartmentRepository;
using EmployeeApp.Repositories.EmployeeRepository;
using EmployeeApp.Repositories.PositionRepository;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace EmployeeApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IEmployee, EmployeeManager>();
            builder.Services.AddTransient<ICompany, CompanyManager>();
            builder.Services.AddTransient<IPosition, PositionManager>();
            builder.Services.AddTransient<IDepartment, DepartmentManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de"),
                },
                SupportedUICultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de"),
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Menu}/{id?}");

            app.Run();
        }
    }
}