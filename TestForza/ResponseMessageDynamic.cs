using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TestForza
{
    public class ResponseMessageDynamic
    {
        public string code { get; set; }
        public string message { get; set; }

        public dynamic data { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        //public IDbTransaction transaction { get; set; }

        public ResponseMessageDynamic(string code, string message, dynamic data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
        //public ResponseMessageDynamic(string code, string message, dynamic data, IDbTransaction transaction)
        //{
        //    this.code = code;
        //    this.message = message;
        //    this.data = data;
        //    this.transaction = transaction;
        //}
    }
}
