namespace app_test_api.Models.Response
{
    public sealed class UserDetailResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public DateTime CreatedAt { get; init; }
        public List<MessageResponse> Messages { get; init; } = new();
    }
}
