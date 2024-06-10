using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Behaviors;
using Notes.Core.Entities;
using Notes.Infrastructure;
using Notes.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Notes.Api.Middlewares;
using Notes.Api.Notes.Commands;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var connectionString =  
    "User ID=postgres;Password=psql;Server=localhost;Port=5432;Database=Notes;Include Error Detail=true";  
services.AddDbContext<ApplicationDbContext>(  
    options => options.UseNpgsql(connectionString));

services.AddControllers();
services.AddRouting();
services.AddTransient<ExceptionHandlingMiddleware>();
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
services.AddValidatorsFromAssemblyContaining<CreateNoteCommandValidator>();

services.AddScoped<IRepository<Note>, Repository<Note>>();

services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", b =>
    {
        b
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     app.UseHsts();
// }
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
