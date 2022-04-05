using EasyTestAPI.Infrastructure.Data;
using EasyTestAPI.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTestAPI.Infrastructure;
public static class StartupSetup
{
  public static void AddDbContext(this IServiceCollection services, string connectionString) =>
      services.AddDbContext<AppDbContext>(options =>
          options.UseNpgsql(connectionString)); // will be created in web project root
  public static void AddWriteDbConnection(this IServiceCollection services) =>
    services.AddScoped<IApplicationWriteDbConnection, ApplicationWriteDbConnection>();
  public static void AddReadDbConnection(this IServiceCollection services) => 
    services.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();
}
