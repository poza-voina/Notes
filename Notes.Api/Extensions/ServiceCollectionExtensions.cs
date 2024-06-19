using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Infrastructure;
using Notes.Infrastructure.Repositories;
using Notes.Infrastructure.Services;

namespace Notes.Api.Extenstions;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(  
            options => options.UseNpgsql(connectionString));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITagRepository, TagRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITagsService, TagsService>();
    }
    
}