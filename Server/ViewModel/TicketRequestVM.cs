using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class TicketRequestVM
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string CategoryName { get; set; }
        public string StatusName { get; set; }
        public string PriorityName { get; set; }
        public string EmployeeName { get; set; }
        public string MessageText { get; set; }
    }
}
