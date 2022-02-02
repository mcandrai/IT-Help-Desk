using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class MessageDetailVM
    {
        public string MessageText { get; set; }
        public string Name { get; set; }
        public string fullName { get; set; }
        public string CreateAt { get; set; }

    }
}
