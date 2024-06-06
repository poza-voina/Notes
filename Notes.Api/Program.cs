using Microsoft.EntityFrameworkCore;
using Notes.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var connectionString =  
    "User ID=postgres;Password=psql;Server=localhost;Port=5432;Database=Notes;Include Error Detail=true";  
services.AddDbContext<ApplicationDbContext>(  
    options => options.UseNpgsql(connectionString));



var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.Run();
