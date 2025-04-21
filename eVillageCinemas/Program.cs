using eVillageCinemas.Models;
using eVillageCinemas.Services.Abstruct;
using eVillageCinemas.Services.AbstructCode;
using eVillageCinemas.Services.ImplementCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IAvailableDateService, AvailableDateService>();
builder.Services.AddScoped<IAvailableSeatService, AvailableSeatService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IHelperService, HelperService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

var connectionString = builder.Environment.IsDevelopment() ?
    builder.Configuration.GetConnectionString("Local") :
    builder.Configuration.GetConnectionString("Live");

builder.Services.AddDbContext<DatabaseContext>(options => options
    .UseLazyLoadingProxies()
    .UseSqlServer(connectionString)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
