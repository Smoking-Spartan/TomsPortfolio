using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptInController : ControllerBase
    {
        private readonly ILogger<OptInController> _logger;
        private readonly AppDbContext _context;

        public OptInController(ILogger<OptInController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Adds a new contact who opts in for SMS messages.
        /// </summary>
        /// <param name="contact">The contact to add.</param>
        /// <returns>The created contact.</returns>
        [HttpPost]
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

                contact.OptInTime = DateTime.UtcNow; // Set the opt-in time
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContacts), new { id = contact.Id }, contact);
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
    }
}