using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("tb_m_tickets")]
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public virtual ProblemCategory Category { get; set; }
        public string CategoryId { get; set; }
        public virtual Status Status { get; set; }
        public string StatusId { get; set; }
        public virtual Employee Employee { get; set; }
        public string NIK { get; set; }
    }
}
