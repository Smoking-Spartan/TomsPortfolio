using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;
using server.Models;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptInController : ControllerBase
    {
        private readonly ILogger<OptInController> _logger;
        private readonly AppDbContext _context;
        private readonly IMessagingService _messagingService;

        public OptInController(ILogger<OptInController> logger, AppDbContext context, IMessagingService messagingService)
        {
            _logger = logger;
            _context = context;
            _messagingService = messagingService;
        }

        /// <summary>
        /// Adds a new contact who opts in for SMS messages.
        /// </summary>
        /// <param name="contact">The contact to add.</param>
        /// <returns>The created contact.</returns>
        [HttpPost("AddContact")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Contact>> AddContact(Contact contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Trim both name and phone number
                contact.Name = contact.Name?.Trim();
                contact.PhoneNumber = contact.PhoneNumber?.Trim();

                // Ensure phone number has + prefix
                if (!contact.PhoneNumber.StartsWith("+"))
                {
                    contact.PhoneNumber = "+" + contact.PhoneNumber;
                }

                // Validate phone number format
                if (!System.Text.RegularExpressions.Regex.IsMatch(contact.PhoneNumber, @"^\+[1-9]\d{1,14}$"))
                {
                    return BadRequest("Invalid phone number format. Must be in E.164 format (e.g., +12025551234)");
                }

                // Check for existing contact with this phone number
                var existingContact = await _context.Contacts
                    .FirstOrDefaultAsync(c => c.PhoneNumber == contact.PhoneNumber);

                if (existingContact == null)
                {
                    // New contact
                    contact.OptInTime = DateTime.UtcNow;
                    contact.LastActiveTime = DateTime.UtcNow;
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(AddContact), new { id = contact.Id }, contact);
                }
                else
                {
                    // Update existing contact's name and ensure they're active
                    existingContact.Name = contact.Name;
                    existingContact.IsActive = true;
                    existingContact.LastActiveTime = DateTime.UtcNow;
                    existingContact.OptOutTime = DateTime.MinValue; // Reset opt-out if they were opted out
                    await _context.SaveChangesAsync();
                    return Ok(existingContact);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding contact");
                return StatusCode(500, "An error occurred while adding the contact");
            }
        }

        /// <summary>
        /// Retrieves all opted-in contacts.
        /// </summary>
        /// <returns>List of opted-in contacts.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Contact>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            try
            {
                var contacts = await _context.Contacts.ToListAsync();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving contacts");
                return StatusCode(500, "An error occurred while retrieving contacts");
            }
        }

        /// <summary>
        /// Removes a contact by ID.
        /// </summary>
        /// <param name="id">The ID of the contact to remove.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveContact(int id)
        {
            try
            {
                var contact = await _context.Contacts.FindAsync(id);
                if (contact == null)
                {
                    return NotFound();
                }

                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing contact");
                return StatusCode(500, "An error occurred while removing the contact");
            }
        }

        /// <summary>
        /// Sends a text message to a contact
        /// </summary>
        /// <param name="phoneNumber">The phone number to send to</param>
        /// <param name="messageContent">The message content</param>
        /// <returns>Status of the message send</returns>
        [HttpPost("SendText")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendText([FromBody] SendTextRequest request)
        {
            try
            {
                // Basic validation
                if (string.IsNullOrEmpty(request.PhoneNumber) || string.IsNullOrEmpty(request.MessageContent))
                {
                    return BadRequest("Phone number and message content are required");
                }

                // Ensure phone number has + prefix
                if (!request.PhoneNumber.StartsWith("+"))
                {
                    request.PhoneNumber = "+" + request.PhoneNumber;
                }

                // Validate phone number format
                if (!System.Text.RegularExpressions.Regex.IsMatch(request.PhoneNumber, @"^\+[1-9]\d{1,14}$"))
                {
                    return BadRequest("Invalid phone number format. Must be in E.164 format (e.g., +12025551234)");
                }

                
                // Check if this phone number has opted in
                var contact = await _context.Contacts
                    .FirstOrDefaultAsync(c => c.PhoneNumber == request.PhoneNumber && c.IsActive);
                
                if (contact == null)
                {
                    return BadRequest("This phone number has not opted in to receive messages");
                }

                // Check if we've sent too many messages recently
                var recentMessages = await _context.Messages
                    .Where(m => m.PhoneNumber == request.PhoneNumber && 
                               m.SentAt > DateTime.UtcNow.AddHours(-1))
                    .CountAsync();

                if (recentMessages >= 3)
                {
                    return BadRequest("Too many messages sent recently. Please try again later.");
                }

                // Create message record
                var message = new Message
                {
                    PhoneNumber = request.PhoneNumber,
                    Content = request.MessageContent,
                    SentAt = DateTime.UtcNow,
                    ContactId = contact.Id
                };

                try
                {
                    // Send the message
                    await _messagingService.SendMessageAsync(request.PhoneNumber, request.MessageContent);
                    message.DeliveredAt = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending message via Vonage");
                    message.ErrorMessage = ex.Message;
                    // Don't throw here, we want to save the error message
                }
                
                // Save message record regardless of success/failure
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(message.ErrorMessage))
                {
                    return StatusCode(500, new { error = "Message failed to send", details = message.ErrorMessage });
                }

                return Ok(new { message = "Text message sent successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendText endpoint");
                return StatusCode(500, "An error occurred while sending the text message");
            }
        }
        /// <summary>
        /// Test for Postman verifying if the server is reachable
        /// </summary>
        /// <returns></returns>
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "pong", timestamp = DateTime.UtcNow });
        }
        
        public class SendTextRequest
        {
            public string PhoneNumber { get; set; } = "";
            public string MessageContent { get; set; } = "";
        }
    }
}