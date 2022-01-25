using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("tb_tr_messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string MessageText { get; set; }
        public virtual Ticket Ticket { get; set; }
        public int TicketId { get; set; }
    }
}
