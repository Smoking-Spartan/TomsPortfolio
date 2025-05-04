using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using server.data;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<TestController> _logger;

    public TestController(IConfiguration configuration, AppDbContext dbContext, ILogger<TestController> logger)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        try
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed");
            return StatusCode(500, new { status = "error", message = ex.Message });
        }
    }

    [HttpGet("database")]
    public async Task<IActionResult> TestDatabase()
    {
        try
        {
            // Test database connection
            await _dbContext.Database.CanConnectAsync();
            
            // Get database connection string (masked for security)
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var maskedConnectionString = MaskConnectionString(connectionString);

            return Ok(new { 
                status = "connected",
                connectionString = maskedConnectionString,
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database connection test failed");
            return StatusCode(500, new { 
                status = "error",
                message = ex.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }

    [HttpGet("sample-data")]
    public async Task<IActionResult> GetSampleData()
    {
        try
        {
            // Get a sample of data from your database
            var sampleData = await _dbContext.Contacts
                .Take(5)
                .Select(c => new { c.Id, c.Name, c.PhoneNumber, c.IsActive })
                .ToListAsync();

            return Ok(new { 
                status = "success",
                data = sampleData,
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Sample data retrieval failed");
            return StatusCode(500, new { 
                status = "error",
                message = ex.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }

    // [HttpPost("api/admin/kill-sms")]
    // public IActionResult SetKillSwitch([FromBody] KillSwitchRequest request)
    // {
    //     var adminPassword = Environment.GetEnvironmentVariable("KillSmsPassword:Password");
    //     if (request.Password != adminPassword)
    //         return Unauthorized();

    //     // Update kill switch in DB

    //     return Ok();
    // }

    [HttpPost("admin/login")]
    public async Task<IActionResult> LogInToKillSwitch([FromBody] AdminLoginRequest request){
        var adminPassword = _configuration["KillSmsPassword:Password"];
        if (request.Password != adminPassword)
            return Unauthorized();
        
        var smsStatus = await _dbContext.SmsStatuses.FirstOrDefaultAsync();

        return Ok(new { IsSmsactive = smsStatus?.IsSmsActive ?? false});
    }

    private string MaskConnectionString(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            return "No connection string found";

        // Mask sensitive parts of the connection string
        var masked = connectionString;
        var sensitivePatterns = new[]
        {
            @"Password=[^;]+",
            @"User ID=[^;]+",
            @"User=[^;]+",
            @"Pwd=[^;]+"
        };

        foreach (var pattern in sensitivePatterns)
        {
            masked = System.Text.RegularExpressions.Regex.Replace(
                masked,
                pattern,
                match => match.Value.Split('=')[0] + "=*****"
            );
        }

        return masked;
    }
    public class AdminLoginRequest
    {
        public string Password { get; set; }
    }
} 