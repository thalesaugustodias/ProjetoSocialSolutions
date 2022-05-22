using Microsoft.EntityFrameworkCore;
using ProjetoSocialSolutions.Data;
using ProjetoSocialSolutions.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>
    (options => options.UseSqlServer("Data Source=THALESDIAS\\SQLEXPRESS;Initial Catalog=ProjetoSocialSolutions;Integrated Security=True"));
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ImovelService>();


    

var app = builder.Build();

var ptBr = new CultureInfo("pt-BR");
var localizacao = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ptBr),
    SupportedCultures = new List<CultureInfo> { ptBr },
    SupportedUICultures = new List<CultureInfo> { ptBr }
};
app.UseRequestLocalization(localizacao);


// Configure the HTTP request pipeline.
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
