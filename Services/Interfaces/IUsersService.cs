using app_test_api.Models;
using app_test_api.Models.Request;
using app_test_api.Models.Response;

namespace app_test_api.Services.Interfaces
{
    public interface IUsersService
    {
        Task<List<UserDetailResponse>> GetAllAsync();
        Task<UserDetailResponse?> GetByIdAsync(int id);
        Task<UserResponse> CreateAsync(CreateUserRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
