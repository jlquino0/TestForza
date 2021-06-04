using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestForza.Commands.Handlers
{
    public class InventarioCommandHandler
    {
        IConfiguration configuration;
        public InventarioCommandHandler(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public async Task<ResponseMessageDynamic> Handle(CompraCommand command)
        {
            
            try
            {
                var cs = @configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;

                using var con = new SqlConnection(cs);
                con.Open();

                //var query = "INSERT INTO cars(name, price) VALUES(@name, @price)";
                var query = "INSERT INTO COMPRA(COMPRA_ID_PRODUCTO, COMPRA_ID_SUCURSAL, COMPRA_CANTIDAD, COMPRA_FECHA) VALUES(@COMPRA_ID_PRODUCTO, @COMPRA_ID_SUCURSAL, @COMPRA_CANTIDAD, GETDATE())";

                var dp = new DynamicParameters();
                dp.Add("@COMPRA_ID_PRODUCTO", command.id_producto);
                dp.Add("@COMPRA_ID_SUCURSAL", command.id_sucursal);
                dp.Add("@COMPRA_CANTIDAD", command.cantidad);

                int res = con.Execute(query, dp);

                var queryUpd = "UPDATE INVENTARIO SET INVENTARIO_STOCK = INVENTARIO_STOCK + @COMPRA_CANTIDAD WHERE INVENTARIO_ID_PRODUCTO = @COMPRA_ID_PRODUCTO AND INVENTARIO_ID_SUCURSAL = @COMPRA_ID_SUCURSAL";


                int resUPD = con.Execute(queryUpd, dp);

                if (res > 0)
                {
                    Console.WriteLine("row inserted");
                }
            }
            catch (Exception ex)
            {
                //command.transaction.Rollback();
                //throw ex;
                return new ResponseMessageDynamic(DefResponseMessage.DEF_ERR_NUM,
                    DefResponseMessage.DEF_ERR_MSG + " - " + ex.ToString(),
                    null);
            }

            return new ResponseMessageDynamic(DefResponseMessage.DEF_SUCCESS_NUM,
                    DefResponseMessage.DEF_SUCCESS_MSG,
                    null);
        }


        public async Task<ResponseMessageDynamic> Handle(VentaCommand command)
        {

            try
            {
                var cs = @configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;

                using var con = new SqlConnection(cs);
                con.Open();

                //var query = "INSERT INTO cars(name, price) VALUES(@name, @price)";
                var query = "INSERT INTO VENTA(VENTA_ID_PRODUCTO, VENTA_ID_SUCURSAL, VENTA_CANTIDAD, VENTA_FECHA) VALUES(@VENTA_ID_PRODUCTO, @VENTA_ID_SUCURSAL, @VENTA_CANTIDAD, GETDATE())";

                var dp = new DynamicParameters();
                dp.Add("@VENTA_ID_PRODUCTO", command.id_producto);
                dp.Add("@VENTA_ID_SUCURSAL", command.id_sucursal);
                dp.Add("@VENTA_CANTIDAD", command.cantidad);

                int res = con.Execute(query, dp);

                var queryUpd = "UPDATE INVENTARIO SET INVENTARIO_STOCK = INVENTARIO_STOCK - @VENTA_CANTIDAD WHERE INVENTARIO_ID_PRODUCTO = @VENTA_ID_PRODUCTO AND INVENTARIO_ID_SUCURSAL = @VENTA_ID_SUCURSAL";


                int resUPD = con.Execute(queryUpd, dp);

                if (res > 0)
                {
                    Console.WriteLine("row inserted");
                }
            }
            catch (Exception ex)
            {
                //command.transaction.Rollback();
                //throw ex;
                return new ResponseMessageDynamic(DefResponseMessage.DEF_ERR_NUM,
                    DefResponseMessage.DEF_ERR_MSG + " - " + ex.ToString(),
                    null);
            }

            return new ResponseMessageDynamic(DefResponseMessage.DEF_SUCCESS_NUM,
                    DefResponseMessage.DEF_SUCCESS_MSG,
                    null);
        }
    }
}
