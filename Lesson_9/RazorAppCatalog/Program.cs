using RazorAppCatalog.Services.Email;
using Microsoft.Extensions.Options;
using Serilog;
using RazorAppCatalog.Services.Background;
using RazorAppCatalog.DomainEvents.Handlers;
using RazorAppCatalog.Entities;
using Microsoft.AspNetCore.HttpLogging;
using RazorAppCatalog.Middleware.MetricMiddleware;
using RazorAppCatalog.Middleware.MetricMiddleware.MetricManager;

Log.Logger = new LoggerConfiguration()
   .WriteTo.Console()
   .CreateBootstrapLogger(); //означает, что глобальный логер будет заменен на вариант из Host.UseSerilog
Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.Configure<SmtpConfig>(
                 builder.Configuration.GetSection("SmtpConfig"));
    builder.Services.Configure<SmtpConfig>(opt =>
    {
        opt.Password = builder.Configuration["SmtpConfig:UserPassword"];
        opt.UserName = builder.Configuration["SmtpConfig:UserName"];

    });

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddHostedService<TestBackgroundService>();
    builder.Services.AddHostedService<ProductAddedEmailSender>();

    // ¬ыбран Singleton, так как Catalog потокобезопасен, при запуске будет создан 1 объект данного класса, а врем€ жизни равно времени жизни приложени€ (api).
    builder.Services.AddSingleton<ICatalog, Catalog>();
    builder.Services.AddSingleton<IEmailService, EmailService>();
    builder.Services.AddSingleton<IMetric, Metric>();


    builder.Host.UseSerilog((ctx, conf) =>
    {
        conf
           .MinimumLevel.Debug()
           .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
           .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
           .ReadFrom.Configuration(ctx.Configuration)
       ;
    });

    builder.Services.AddHttpLogging(options =>
    {
        options.LoggingFields = HttpLoggingFields.RequestBody
                                | HttpLoggingFields.ResponseBody;
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpLogging();

    app.UseHttpsRedirection();

    app.UseMiddleware<MetricMiddleware>();

    app.Use(async (HttpContext context, Func<Task> next) =>
    {
        var userAgent = context.Request.Headers.UserAgent.ToString();

        if (!userAgent.Contains("Edg"))
        {
            context.Response.ContentType = "text/plain; charset=UTF-8";
            await context.Response.WriteAsync("¬аш браузер не поддерживаетс€. ѕоддерживаетс€ только Edge.");

            return;
        }

        await next();
    });

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

    Log.Fatal(ex, "—ервер рухнул!");
    throw;
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush(); //перед выходом дожидаемс€ пока все логи будут записаны
}
