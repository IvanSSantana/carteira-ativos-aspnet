using CarteiraAtivos.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using CarteiraAtivos.Repositories;
using CarteiraAtivos.Services;
using CarteiraAtivos.Helpers;
using Microsoft.Extensions.DependencyInjection;

Env.Load("./Environment/.env"); // Pega as variáveis de ambiente do arquivo .env

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(Env.GetString("DB_CONNECTION")));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IApiFinanceiraService, ApiFinanceiraService>();

builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<AtivoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IAtivoRepositorio, AtivoRepositorio>();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var mvcBuilder = builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginUsuario}/{action=Index}/{id?}");

app.Run();
