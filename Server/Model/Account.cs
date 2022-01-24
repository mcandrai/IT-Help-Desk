using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Model
{
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual Employee Employee { get; set; }
        public int OTPCode { get; set; }
        public bool OTPStatus { get; set; }
        public DateTime OTPExpired { get; set; }
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}
