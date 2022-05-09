using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskManagement;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");
   /* app.UseExceptionHandler(c => c.Run(async context =>
    {
        Console.WriteLine("hello hello");
        *//*var exception = context.Features
            .Get<IExceptionHandlerPathFeature>()
            .Error;
        var response = new { error = exception.Message };
        await context.Response.WriteAsJsonAsync(response);*//*
    }));
    *//* app.UseExceptionHandler("/Home/Error");*/
}

var supportedCoultures = new[]
{
    new CultureInfo("ru"),
    new CultureInfo("en")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en"),
    SupportedCultures = supportedCoultures,
    SupportedUICultures = supportedCoultures
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();