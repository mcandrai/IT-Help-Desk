using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("tb_tr_message_detail")]
    public class MessageDetail
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual Message Message { get; set; }
        public int MessageId { get; set; }
        public string MessageText { get; set; }
    }
}
