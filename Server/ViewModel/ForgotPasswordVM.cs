using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class ForgotPasswordVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int OTPCode { get; set; }
        public bool OTPStatus { get; set; }
        public DateTime OTPExpired { get; set; }
    }
}
