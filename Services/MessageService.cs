using app_test_api.Data;
using app_test_api.Models;
using app_test_api.Models.Request;
using app_test_api.Models.Response;
using app_test_api.Services.Interfaces;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace app_test_api.Services;

public class MessageService : IMessageService
{
    private readonly AppDbContext _context;

    public MessageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MessageResponse>> GetAllAsync()
    {
        return await _context.Messages
            .AsNoTracking()
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new MessageResponse
            {
                Id = m.Id,
                MessageContent = m.MessageContent,
                Sender = m.Sender,
                CreatedAt = m.CreatedAt,
                User = new UserResponse
                {
                    Id = m.User.Id,
                    Name = m.User.Name
                }
            })
            .ToListAsync();
    }

    public async Task<MessageResponse?> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        return await _context.Messages
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new MessageResponse
            {
                Id = m.Id,
                MessageContent = m.MessageContent,
                Sender = m.Sender,
                CreatedAt = m.CreatedAt,
                User = new UserResponse
                {
                    Id = m.User.Id,
                    Name = m.User.Name
                }
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MessageResponse> CreateAsync(CreateMessageRequest message)
    {
        var recipientExists = await _context.Users
            .AnyAsync(u => u.Id == message.RecipientId);

        if (!recipientExists)
        {
            throw new InvalidOperationException($"Recipient with ID {message.RecipientId} does not exist");
        }

        message.MessageContent = message.MessageContent.Trim();
        message.Sender = message.Sender.Trim();

        var messageEntity = new Message
        {
            MessageContent = message.MessageContent,
            Sender = message.Sender,
            RecipientId = message.RecipientId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Messages.Add(messageEntity);
        await _context.SaveChangesAsync();

        var user = await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == message.RecipientId)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name
            })
            .FirstOrDefaultAsync();

        return new MessageResponse
        {
            Id = messageEntity.Id,
            MessageContent = messageEntity.MessageContent,
            Sender = messageEntity.Sender,
            CreatedAt = messageEntity.CreatedAt,
            User = user ?? new UserResponse()
        };
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
        {
            return;
        }

        var message = await _context.Messages.FindAsync(id);
        if (message != null)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}
