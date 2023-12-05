using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject329A.ViewModel
{
    public class VMResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }

        public VMResponse()
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = string.Empty;
            data = null;
        }
    }
}
