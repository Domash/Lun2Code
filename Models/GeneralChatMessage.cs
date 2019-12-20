using System;

namespace Lun2Code.Models
{
    public class GeneralCharMessage
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public string UserEmail { get; set; }
        
        public string Text { get; set; }
        
        public DateTime Published { get; set; }
        
        
    }
}