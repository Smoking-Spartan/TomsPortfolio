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

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:11534")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IMessagingService, MessagingService>();

// Load configuration
var configuration = builder.Configuration;

// Create credentials
var credentials = Credentials.FromApiKeyAndSecret(
    configuration["ApiKey"],
    configuration["ApiSecret"]
);

builder.Services.AddSingleton(sp =>
{
    var credentials = Credentials.FromApiKeyAndSecret(
        configuration["ApiKey"],
        configuration["ApiSecret"]
    );
    return new VonageClient(credentials);
});

// Register MessagingService with the phone number from configuration
builder.Services.AddScoped<IMessagingService>(sp => 
    new MessagingService(
        sp.GetRequiredService<VonageClient>(),
        configuration["VonagePhoneNumber"],
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
