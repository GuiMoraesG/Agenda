using Microsoft.EntityFrameworkCore;
using Agenda.Data;
using Agenda.Services;
using Agenda.Helper;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("AgendaContext"), new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<Session>();

builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
