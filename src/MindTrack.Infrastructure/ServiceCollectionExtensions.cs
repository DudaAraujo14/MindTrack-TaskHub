using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MindTrack.Application.Interfaces;
using MindTrack.Infrastructure.Persistence;
using MindTrack.Infrastructure.Repositories; 

namespace MindTrack.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, string? connString)
        {
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlite(connString ?? "Data Source=MindTrack.db"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IFocusRecordRepository, FocusRecordRepository>(); // ✅ agora vai funcionar

            return services;
        }
    }
}
