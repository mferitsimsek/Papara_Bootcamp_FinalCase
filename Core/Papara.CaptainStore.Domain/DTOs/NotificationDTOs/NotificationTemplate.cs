namespace Papara.CaptainStore.Domain.DTOs.NotificationDTOs
{
    public class NotificationTemplate
    {
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public required string RecipientEmail { get; set; }
    }
}
