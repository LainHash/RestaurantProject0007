using Restaurant.BlazorApp.Components;
using Restaurant.BlazorApp.Services.Abstraction;
using Restaurant.BlazorApp.Services.Abstraction.Catalog;
using Restaurant.BlazorApp.Services.Concrete;
using Restaurant.BlazorApp.Services.Concrete.Catalog;
using System.IO;
using System;

// Find .env file from current directory, going up
var searchDir = Directory.GetCurrentDirectory();
while (searchDir is not null)
{
    var envPath = Path.Combine(searchDir, ".env");
    if (File.Exists(envPath)) { DotNetEnv.Env.Load(envPath); break; }
    searchDir = Directory.GetParent(searchDir)?.FullName;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiBaseUrl = Environment.GetEnvironmentVariable("WebAPI__BaseUrl") ?? "https://localhost:7006";

builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
