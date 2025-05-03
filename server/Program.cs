using Microsoft.EntityFrameworkCore;
using server.data;
using server.Services;
using Vonage.Request;
using Vonage;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add Serilog as the logging provider
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog(dispose: true);
});

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? 
            new[] { "http://localhost:11534" };  // Default to localhost if not configured

        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IMessagingService, MessagingService>();

// Register SurveyService
builder.Services.AddScoped<ISurveyService, SurveyService>();

// Load configuration
var configuration = builder.Configuration;

// Register Vonage client as singleton
builder.Services.AddSingleton<VonageClient>(sp => 
{
    var credentials = Credentials.FromApiKeyAndSecret(
        configuration["ApiKey"],
        configuration["ApiSecret"]
    );
    return new VonageClient(credentials);
});

// Register MessagingService as scoped
builder.Services.AddScoped<IMessagingService>(sp => 
    new MessagingService(
        sp.GetRequiredService<VonageClient>(),
        configuration["VonagePhoneNumber"] ?? "",
        sp.GetRequiredService<ILogger<MessagingService>>()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Easier debugging in development
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.UseAuthorization();

app.MapControllers();

app.Run();
