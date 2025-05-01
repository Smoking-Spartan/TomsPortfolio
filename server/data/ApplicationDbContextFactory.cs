using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using server.data;

namespace TomsPortfolio.Server.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Load configuration from appsettings.json and environment-specific files
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        string provider = configuration.GetValue<string>("DatabaseProvider") ?? "SQLServer";
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        switch (provider.ToLower())
        {
            case "sqlite":
                optionsBuilder.UseSqlite(connectionString, 
                    options => options.MigrationsAssembly("TomsPortfolio.Server"));
                break;
            
            default: // SQL Server
                optionsBuilder.UseSqlServer(connectionString,
                    options => options.MigrationsAssembly("TomsPortfolio.Server"));
                break;
        }

        return new AppDbContext(optionsBuilder.Options);
    }
} 