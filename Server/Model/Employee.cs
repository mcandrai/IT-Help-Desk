using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("tb_m_employees")]
    public class Employee
    {
        [Key]//penunjuk primary key
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<MessageDetail> MessageDetail { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
