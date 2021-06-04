using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data;
//using Oracle.ManagedDataAccess.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TestForza.Models;
//using TestForza.Persistence;
using TestForza.Properties;
using System.Data.SqlClient;
using System.Dynamic;

namespace TestForza.Queries.Handlers
{
    public class LoginQueryHandler : LoginDao
    {
        IConfiguration configuration;
        public LoginQueryHandler(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public ResponseMessageDynamic Handle(LoginQuery query)
        {
            IEnumerable<LoginRead> result = null;
            try
            {

                var cs = @configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;

                using var con = new SqlConnection(cs);
                con.Open();

                var userQ = con.QueryFirst<LoginRead>("SELECT * FROM dbo.USERS WHERE USER_USUARIO = @USER_USUARIO AND USER_PASS = @USER_PASS",
                    new { USER_USUARIO = query.user, USER_PASS = query.pass });

                Console.WriteLine(userQ);

                //if (result.Count() > 0)
                    if (userQ.USER_PASS == query.pass)
                    {
                        var user = userQ;
                        user.TOKEN = this.generateJwtToken(user.USER_ID);
                        //return user;
                        return new ResponseMessageDynamic(DefResponseMessage.DEF_SUCCESS_NUM,
                        DefResponseMessage.DEF_SUCCESS_MSG,
                        user);
                    }

                //}
            }
            catch (Exception ex)
            {
                //throw ex;
                return new ResponseMessageDynamic(DefResponseMessage.DEF_ERR_NUM,
                    DefResponseMessage.DEF_ERR_MSG + " - User Not Found.",
                    null);
            }
            //return null;
            return new ResponseMessageDynamic(DefResponseMessage.DEF_ERR_NUM,
                    DefResponseMessage.DEF_ERR_MSG + " - User Not Found.",
                    null);
        }

        public AuthorizedUser Handle(int USER_ID)
        {
            IEnumerable<LoginRead> result = null;
            try
            {

                var cs = @configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;

                using var con = new SqlConnection(cs);
                con.Open();

                var userQ = con.QueryFirst<LoginRead>("SELECT * FROM dbo.USERS WHERE USER_ID = @USER_ID",
                    new { USER_ID = USER_ID });

                Console.WriteLine(userQ);

                //return user;
                return userQ;

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return null;
        }

        private string generateJwtToken(int user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings").GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
