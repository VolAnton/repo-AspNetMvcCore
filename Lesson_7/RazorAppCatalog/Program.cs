using RazorAppCatalog.Services.Email;
using RazorAppCatalog.ViewModels;
using Microsoft.Extensions.Options;
using Serilog;
using RazorAppCatalog.Services.Background;

Log.Logger = new LoggerConfiguration()
   .WriteTo.Console()
   .CreateBootstrapLogger(); //означает, что глобальный логер будет заменен на вариант из Host.UseSerilog
Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.Configure<SmtpConfig>(
                 builder.Configuration.GetSection("SmtpConfig"));


    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddHostedService<TestBackgroundService>();

    // Выбран Singleton, так как Catalog потокобезопасен, при запуске будет создан 1 объект данного класса, а время жизни равно времени жизни приложения (api).
    builder.Services.AddSingleton<ICatalog, Catalog>();

    builder.Services.AddSingleton<IEmailService, EmailService>();

    builder.Host.UseSerilog((ctx, conf) =>
    {
        conf
           .MinimumLevel.Debug()
           .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
           .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
           .ReadFrom.Configuration(ctx.Configuration)
       ;
    });


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseSerilogRequestLogging();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    if (ex.GetType().Name is "StopTheHostException")
    {
        throw;
    }

    Log.Fatal(ex, "Сервер рухнул!");
    throw;
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush(); //перед выходом дожидаемся пока все логи будут записаны
}
