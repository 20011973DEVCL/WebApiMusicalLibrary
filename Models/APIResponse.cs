using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiMusicalLibrary
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool Successful { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}