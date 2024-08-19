using Microsoft.EntityFrameworkCore;
using MusicBox.Repository;
using MusicBox.Models;

using MusicBox.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<SongContext>(options => options.UseSqlServer(connection));

// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); 
    options.Cookie.Name = "Session"; 
});

builder.Services.AddScoped<IRepository<Users>, UserRepository>();
builder.Services.AddScoped<IRepository<Songs>, SongRepository>();
builder.Services.AddScoped<IRepository<Genres>, GenreRepository>();


builder.Services.AddScoped<ILangRead, ReadLangServices>();

var app = builder.Build();
app.UseAuthorization();
//app.AddLocalization(options => options.ResourcesPath = "Resources");
app.UseSession();
app.UseStaticFiles();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();


