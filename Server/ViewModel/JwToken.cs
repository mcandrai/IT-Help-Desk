using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server.ViewModel
{
    public class JwToken
    {
        public HttpStatusCode status { get; set; }
        public string idToken { get; set; }
        public string message { get; set; }
    }
}
