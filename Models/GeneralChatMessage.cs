using System;

namespace Lun2Code.Models
{
    public class GeneralChatMessage
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public string UserEmail { get; set; }
        
        public string Text { get; set; }
        
        public DateTime Published { get; set; }
        
        public string UserId { get; set; }
        
        public User User { get; set; }

        GeneralChatMessage()
        {
            Published = DateTime.Now;
        }
        
        
    }
}