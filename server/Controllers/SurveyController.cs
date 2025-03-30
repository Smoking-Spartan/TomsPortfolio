using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.data;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly ILogger<SurveyController> _logger;
        private readonly AppDbContext _context;

        public SurveyController(ILogger<SurveyController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        /// <summary>
        /// Retrieves all contacts
        /// </summary>
        /// <returns>List of contacts</returns>
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
        /// Creates a new contact
        /// </summary>
        /// <param name="contact">The contact to create</param>
        /// <returns>The created contact</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newContact = new Contact
                {
                    Name = contact.Name,
                    PhoneNumber = contact.PhoneNumber,
                    OptInTime = contact.OptInTime
                };

                await _context.Contacts.AddAsync(newContact);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContacts), new { id = newContact.Id }, newContact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating contact");
                return StatusCode(500, "An error occurred while creating the contact");
            }
        }
    }
}
