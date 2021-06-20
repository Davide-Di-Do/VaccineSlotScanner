namespace vaccine_slot_scanner.Models
{
    public class MailgunRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}