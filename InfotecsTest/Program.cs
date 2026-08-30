using CsvHelper;
using InfotecsTest.Data;
using InfotecsTest.Services.Abstract;
using InfotecsTest.Services.Values;
using InfotecsTest.Services.Values.Abstract;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace InfotecsTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var cofigBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllersWithViews()
                .AddOData(options => options
                .Select().Filter().OrderBy().Expand().Count());

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console() 
                .WriteTo.File(
                    path: "logs/log-.txt", 
                    rollingInterval: RollingInterval.Month, 
                    retainedFileCountLimit: 31, 
                    fileSizeLimitBytes: 10_000_000, 
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1)
                )
                .CreateLogger();

            builder.Host.UseSerilog(); 

            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IValuesCsvParser, ValuesCsvParser>();
            builder.Services.AddScoped<IValuesCsvMetricCalculator, ValuesCsvMetricCalculator>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
