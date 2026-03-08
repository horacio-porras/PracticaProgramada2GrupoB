using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using PracticaProgramada2BLL;
using PracticaProgramada2BLL.Servicios.Categoria;
using PracticaProgramada2BLL.Servicios.Producto;
using PracticaProgramada2DAL.Data;
using PracticaProgramada2DAL.Repositorios.Generico;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuracion de la conexion a la base de datos SQL LITE
builder.Services.AddDbContext<PracticaProgramada2DbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


//  Inyeccion de dependencias de las interfaces que implementamos
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();

builder.Services.AddScoped(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>)); // Repositorio Generico


// Inyeccion de librerias
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));


// Configuracion de secretos y variables de entorno

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

app.UseAuthorization();

app.MapStaticAssets();


app.UseMiddleware<PracticaProgramada2.Middleware.MiddlewareGlobalExceptionHandler>(); //Manejo global de excepciones

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
