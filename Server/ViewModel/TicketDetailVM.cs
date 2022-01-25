using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class TicketDetailVM
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        [JsonIgnore]
        public virtual ProblemCategory Category { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Status Status { get; set; }
        public int StatusId { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        public string NIK { get; set; }
        public string Message { get; set; }
    }
}
