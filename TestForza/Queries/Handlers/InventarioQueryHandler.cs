using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestForza.Queries.Handlers
{
    public class InventarioQueryHandler
    {
        IConfiguration configuration;
        public InventarioQueryHandler(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public ResponseMessageDynamic Handle(ReporteGeneral query)
        {
            try
            {

                var cs = @configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;

                using var con = new SqlConnection(cs);
                con.Open();

                var retult = con.Query<dynamic>("SELECT PRODUCTO.PRODUCTO_ID, PRODUCTO_DESCRIPCION, INVENTARIO_STOCK, SUCURSAL_NOMBRE FROM INVENTARIO INNER JOIN PRODUCTO ON INVENTARIO.INVENTARIO_ID_PRODUCTO = PRODUCTO_ID INNER JOIN SUCURSAL ON SUCURSAL.SUCURSAL_ID = INVENTARIO.INVENTARIO_ID_SUCURSAL");

                return new ResponseMessageDynamic(DefResponseMessage.DEF_SUCCESS_NUM,
                DefResponseMessage.DEF_SUCCESS_MSG,
                retult);
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

        public ResponseMessageDynamic Handle(ReporteSucursal query)
        {
            try
            {

                var cs = @configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;

                using var con = new SqlConnection(cs);
                con.Open();

                var retult = con.Query<dynamic>("SELECT PRODUCTO.PRODUCTO_ID, PRODUCTO_DESCRIPCION, INVENTARIO_STOCK, SUCURSAL_NOMBRE FROM INVENTARIO INNER JOIN PRODUCTO ON INVENTARIO.INVENTARIO_ID_PRODUCTO = PRODUCTO_ID INNER JOIN SUCURSAL ON SUCURSAL.SUCURSAL_ID = INVENTARIO.INVENTARIO_ID_SUCURSAL WHERE SUCURSAL.SUCURSAL_ID = @SUCURSAL_ID", new { SUCURSAL_ID = query.sucursal_id });

                return new ResponseMessageDynamic(DefResponseMessage.DEF_SUCCESS_NUM,
                DefResponseMessage.DEF_SUCCESS_MSG,
                retult);
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
    }
}
