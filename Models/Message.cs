using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_test_api.Models;

[Table("Messages")]
public class Message
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Sender { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Recipient { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string MessageContent { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
