using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForza.Commands
{
    public class VentaCommand
    {
        public string id_producto { get; set; }
        public string id_sucursal { get; set; }
        public string cantidad { get; set; }
    }
}
