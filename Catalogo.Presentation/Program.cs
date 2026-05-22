using Catalogo.Application.Services;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

// --- NUEVO 1: Librería necesaria para el login con Cookies ---
using Microsoft.AspNetCore.Authentication.Cookies;
// -----------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// Ruta del archivo JSON — se guarda en la carpeta "data" del proyecto
var jsonPath = Path.Combine(builder.Environment.ContentRootPath, "data", "items.json");

// Registrar el repositorio JSON como implementación de IItemRepository
builder.Services.AddSingleton<IItemRepository>(new JsonItemRepository(jsonPath));

// Registrar el servicio de Application
builder.Services.AddScoped<ItemService>();

// --- NUEVO 2: Activa el motor de Login (No afecta a tu IItemRepository) ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Cuenta/Login"; // Redirige aquí si no hay sesión
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });
// --------------------------------------------------------------------------

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// --- NUEVO 3: Le dice a la app que verifique las sesiones activas ---
app.UseAuthentication();
// --------------------------------------------------------------------

app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();