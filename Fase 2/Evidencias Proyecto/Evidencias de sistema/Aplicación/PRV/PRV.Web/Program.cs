using PRV.Web.Repositories;
using PRV.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Agregar Session
builder.Services.AddSession();

//Agregar los servicios y repositorios
builder.Services.AddScoped<EmpresaRepository>();
builder.Services.AddScoped<EmpresaService>();

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<ProductoService>();

builder.Services.AddScoped<VentaRepository>();
builder.Services.AddScoped<VentaService>();

builder.Services.AddScoped<CompraRepository>();
builder.Services.AddScoped<CompraService>();

builder.Services.AddScoped<CorreoService>();

var connectionString = builder.Configuration.GetConnectionString("PRV_BD");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

// Usar Session
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();