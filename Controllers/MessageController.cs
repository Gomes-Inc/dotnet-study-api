using app_test_api.Models.Request;
using app_test_api.Models.Response;
using app_test_api.Services.Interfaces;
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
    public async Task<ActionResult<List<MessageResponse>>> GetAll()
    {
        var messages = await _messageService.GetAllAsync();
        return Ok(messages);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MessageResponse>> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { message = "Invalid message ID" });
        }

        var message = await _messageService.GetByIdAsync(id);
        if (message == null)
        {
            return NotFound(new { message = $"Message with ID {id} not found" });
        }

        return Ok(message);
    }

    [HttpPost]
    public async Task<ActionResult<MessageResponse>> Create([FromBody] CreateMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.MessageContent))
        {
            return BadRequest(new { message = "Message content is required" });
        }

        if (string.IsNullOrWhiteSpace(request.Sender))
        {
            return BadRequest(new { message = "Sender is required" });
        }

        if (request.RecipientId <= 0)
        {
            return BadRequest(new { message = "Valid recipient ID is required" });
        }

        try
        {
            var createdMessage = await _messageService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = createdMessage.Id }, createdMessage);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { message = "Invalid message ID" });
        }

        var message = await _messageService.GetByIdAsync(id);
        if (message == null)
        {
            return NotFound(new { message = $"Message with ID {id} not found" });
        }

        await _messageService.DeleteAsync(id);
        return NoContent();
    }
}
