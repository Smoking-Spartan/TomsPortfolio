using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;

namespace server.Controllers{
[ApiController]
[Route("api/[controller]")]
public class SmsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SmsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetSmsStatus()
    {
        var settings = await _context.SmsStatuses.FirstOrDefaultAsync();
        // Assume IsSmsKilled is true if SMS is OFF
        bool isSmsActive = settings != null && settings.IsSmsActive;
        return Ok(new { isSmsActive });
    }
}
}