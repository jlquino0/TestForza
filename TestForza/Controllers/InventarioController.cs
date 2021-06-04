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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioQueryHandler _InventarioQueryHandler;
        private readonly InventarioCommandHandler _InventarioCommandHandler;

        public InventarioController(InventarioQueryHandler queryHandler, InventarioCommandHandler commandHandler)
        {
            _InventarioQueryHandler = queryHandler;
            _InventarioCommandHandler = commandHandler;
        }

        // POST api/<UsersController>
        [Authorize]
        [HttpGet]
        public ResponseMessageDynamic ReporteGeneral()
        {
            var response = _InventarioQueryHandler.Handle(new ReporteGeneral());

            return response;
        }

        [HttpPost]
        [Authorize]
        public ResponseMessageDynamic ReporteSucursal(ReporteSucursal reporteSucursal)
        {
            var response = _InventarioQueryHandler.Handle(reporteSucursal);

            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<ResponseMessageDynamic> Compra(CompraCommand compra)
        {
            var response = await _InventarioCommandHandler.Handle(compra);

            return response;
        }

        [Authorize]
        [HttpPost]
        public async Task<ResponseMessageDynamic> Venta(VentaCommand venta)
        {
            var response = await _InventarioCommandHandler.Handle(venta);

            return response;
        }

    }
}
