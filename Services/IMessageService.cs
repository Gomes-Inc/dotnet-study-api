using app_test_api.Models;

namespace app_test_api.Services;

public interface IMessageService
{
    Task<List<Message>> GetAllAsync();
    Task<Message?> GetByIdAsync(int id);
    Task<Message> CreateAsync(Message message);
    Task DeleteAsync(int id);
}
