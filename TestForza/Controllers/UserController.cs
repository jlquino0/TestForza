using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestForza.Commands;
using TestForza.Commands.Handlers;
using TestForza.Queries;
using TestForza.Queries.Handlers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestForza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LoginQueryHandler _LoginQueryHandler;

        public UserController(LoginQueryHandler queryHandler)
        {
            _LoginQueryHandler = queryHandler;
        }

        // POST api/<UsersController>
        [HttpPost]
        public ResponseMessageDynamic Post([FromBody] LoginQuery loginQuery)
        {
            var response = _LoginQueryHandler.Handle(loginQuery);

            return response;
        }
    }
}
