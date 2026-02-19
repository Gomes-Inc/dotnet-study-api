using app_test_api.Models;
using app_test_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace app_test_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Message>>> GetAll()
    {
        var messages = await _messageService.GetAllAsync();
        return Ok(messages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetById(int id)
    {
        var message = await _messageService.GetByIdAsync(id);

        if (message == null)
        {
            return NotFound();
        }

        return Ok(message);
    }

    [HttpPost]
    public async Task<ActionResult<Message>> Create([FromBody] CreateMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.MessageContent))
        {
            return BadRequest("Message content is required");
        }

        var message = new Message
        {
            MessageContent = request.MessageContent,
            Sender = request.Sender,
            Recipient = request.Recipient,
            CreatedAt = DateTime.UtcNow
        };

        var createdMessage = await _messageService.CreateAsync(message);
        return CreatedAtAction(nameof(GetById), new { id = createdMessage.Id }, createdMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var message = await _messageService.GetByIdAsync(id);

        if (message == null)
        {
            return NotFound();
        }

        await _messageService.DeleteAsync(id);
        return NoContent();
    }
}
