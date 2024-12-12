using ClassLibrary1;
using Data.Extensions;
using Data.Options;
using Microsoft.AspNetCore;
using violaAPITest;

public class Program
{
    public static void Main(string[] args)
    {
        //CreateHostBuilder(args).Run();
        var host = CreateHostBuilder(args).Build();
        host.Run();

    }
    //var builder = WebApplication.CreateBuilder(args);

    //    // Add services to the container.

    //    builder.Services.AddControllers();
    //        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //        builder.Services.AddEndpointsApiExplorer();
    //        builder.Services.AddSwaggerGen();


    //builder.Services.AddClass();

    //        var app = builder.Build();

    //        // Configure the HTTP request pipeline.
    //        if (app.Environment.IsDevelopment())
    //        {
    //            app.UseSwagger();
    //            app.UseSwaggerUI();
    //        }

    //app.UseHttpsRedirection();

    //app.UseAuthorization();

    //app.MapControllers();

    //app.Run();




    //static IWebHost BuildWebHost(string[] args) =>
    //    WebHost.CreateDefaultBuilder(args)
    //        //.ConfigureServices(services =>
    //        //    services.AddScoped<IMainController, MainController>()
    //        //)
    //        .UseStartup<violaAPITest.Startup>()
    //        .Build();


    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
    }
