using app_test_api.Models;

namespace app_test_api.Services.Interfaces
{
    public interface IUsersService
    {
        Task<List<User>> GetAllAsync();
    }
}
