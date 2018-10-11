using System;

namespace DotNetPractice.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int RecepientId { get; set; }
        public User Recepient { get; set; }
        public string Content { get; set; }
        public bool isRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
        public bool SendersDeleted { get; set; }
        public bool RecepientDeleted { get; set; }
    }
}