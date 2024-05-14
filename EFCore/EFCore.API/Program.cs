using EFCore.Repository;
using EFCore.Services;
using System.Configuration;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EFCore;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddScoped<IValidationService, ValidationService>();
        builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
        builder.Services.AddScoped<IEmployeesService, EmployeesService>();
        builder.Services.AddScoped<IProjectEmployeeService, ProjectEmployeesService>();
        builder.Services.AddScoped<IProjectsService, ProjectsService>();
        builder.Services.AddScoped<ISalariesService, SalariesService>();
        builder.Services.AddDbContext<EFCoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddDbContext<EFCoreContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    //public static IHostBuilder CreateHostBuilder(string[] args) =>
    //        Host.CreateDefaultBuilder(args)
    //            .ConfigureServices((hostContext, services) =>
    //            {
    //                // Configure DbContext
    //                services.AddDbContext<EFCoreContext>(options =>
    //                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));

    //                // Register services and repositories
    //                //services.AddScoped<IExampleRepository, ExampleRepository>();

    //                // Add controllers
    //                services.AddControllers();
    //            });
}

