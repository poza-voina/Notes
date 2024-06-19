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
using Notes.Api.Notes.Queries;
using Notes.Api.Tags.Commands;
using Notes.Api.Extenstions;
using Notes.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var connectionString =  
    "User ID=postgres;Password=psql;Server=localhost;Port=1111;Database=Notes;Include Error Detail=true";
services.AddDbContext(connectionString);

services.AddControllers();
services.AddRouting();

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
services.AddValidatorsFromAssemblyContaining<CreateNoteCommandValidator>();
services.AddValidatorsFromAssemblyContaining<GetNoteQueryValidator>();
services.AddValidatorsFromAssemblyContaining<DeleteNoteCommandValidator>();
services.AddValidatorsFromAssemblyContaining<UpdateNoteCommandValidator>();

services.AddValidatorsFromAssemblyContaining<CreateTagCommandValidator>();
services.AddValidatorsFromAssemblyContaining<GetNoteQueryValidator>();
services.AddValidatorsFromAssemblyContaining<DeleteTagCommandValidator>();
services.AddValidatorsFromAssemblyContaining<UpdateTagCommandValidator>();

services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
services.AddRepositories();
services.AddServices();


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
// app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
