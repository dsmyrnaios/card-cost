using System;
using System.Collections.Generic;
using System.Text;

namespace Cardcost.Domain
{
    public class ApiError
    {
        public string Message { get; set; }
        public string DevMessage { get; set; }
        public string Detail { get; set; }
        
        public ApiError(string message)
        {
            this.Message = message;
        }

        public ApiError() { }
    }
}
