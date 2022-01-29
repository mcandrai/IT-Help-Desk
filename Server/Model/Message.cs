using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("tb_tr_messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string MessageText { get; set; }
        [JsonIgnore]
        public virtual Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        [JsonIgnore]
        public virtual ICollection<MessageDetail> MessageDetail { get; set; }
    }
}
