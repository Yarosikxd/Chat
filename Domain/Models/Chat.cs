namespace Domain.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
    }
}
