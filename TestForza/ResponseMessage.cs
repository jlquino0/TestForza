using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForza
{
    public class ResponseMessage
    {
        public string code { get; set; }
        public string message { get; set; }

        public IEnumerable<Object> data {get;set;}

        public ResponseMessage(string code, string message, IEnumerable<Object> data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
}
