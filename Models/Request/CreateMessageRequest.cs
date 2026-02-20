namespace app_test_api.Models.Request;

public class CreateMessageRequest
{
    public string Sender { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string MessageContent { get; set; } = string.Empty;
}
