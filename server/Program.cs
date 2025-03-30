using Microsoft.EntityFrameworkCore;
using server.data;
using server.Services;
using Vonage.Request;
using Vonage;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=appdata.db"));

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

//var configuration = Settings.Get<VonageSettings>();
//// Create a single instance that will be reused
//var credentials = Credentials.FromApiKeyAndSecret(
//    configuration.VonageAPIKey,
//    configuration.VonageAPISecret
//);
//var vonageClient = new VonageClient(credentials);

// Register the single instance
//builder.Services.AddSingleton(vonageClient);

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
