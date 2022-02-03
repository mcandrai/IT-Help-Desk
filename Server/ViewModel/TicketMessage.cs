using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class TicketMessage
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        [JsonIgnore]
        public virtual ProblemCategory Category { get; set; }
        [JsonIgnore]
        public virtual Status Status { get; set; }
        [JsonIgnore]
        public virtual Priority Priority { get; set; }
        public string CategoryName { get; set; }
        public string StatusName { get; set; }
        public string PriorityName { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public virtual ICollection<MessageDetail> MessageDetail { get; set; }

        public string Image { get; set; }
    }
}
