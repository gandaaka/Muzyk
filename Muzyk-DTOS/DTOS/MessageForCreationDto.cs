using System;

namespace Muzyk_DTOS
{
    public class MessageForCreationDto
    {
        public int SenderId { get; set; }
        public int RecepientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; }
        public MessageForCreationDto()
        {
            MessageSent = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
        }
    }
}