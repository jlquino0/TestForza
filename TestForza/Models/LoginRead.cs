using TestForza.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForza.Models
{
    public interface LoginDao
    {
        ResponseMessageDynamic Handle(LoginQuery query);
        AuthorizedUser Handle(int USER_ID);
    }
    public class LoginRead : AuthorizedUser
    {

        public int USER_ID { get; set; }
        public string USER_NOMBRE1 { get; set; }
        public string USER_NOMBRE2 { get; set; }
        public string USER_APELLIDO1 { get; set; }
        public string USER_APELLIDO2 { get; set; }
        public string USER_TELEFONO { get; set; }
        public string USER_CORREO { get; set; }
        public string USER_USUARIO { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public string USER_PASS { get; set; }

    }
}
