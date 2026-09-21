using Dapper;
using Microsoft.Data.SqlClient;
using PRV.Web.Models;
using System.Data;

namespace PRV.Web.Repositories
{
    public class CompraRepository
    {
        private readonly string _connectionString;

        public CompraRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PRV_BD")!;
        }


        // ==========================================
        // PAGAR COMPRA
        // ==========================================
        public void Pagar(Compra compra)
        {
            using var conexion =
                new SqlConnection(_connectionString);


            // Crear tabla para enviar el detalle a SQL Server
            var detalle = new DataTable();

            detalle.Columns.Add("IdProducto", typeof(long));
            detalle.Columns.Add("Cantidad", typeof(int));


            // Agregar productos de la compra
            foreach (var producto in compra.Detalle)
            {
                detalle.Rows.Add(
                    producto.IdProducto,
                    producto.Cantidad
                );
            }


            // Parámetros del Stored Procedure
            var parametros = new DynamicParameters();

            parametros.Add(
                "@IdUsuario",
                compra.IdUsuario
            );

            parametros.Add(
                "@Detalle",
                detalle.AsTableValuedParameter("dbo.TipoDetalleCompra")
            );


            // Ejecutar pago
            conexion.Execute(
                "dbo.sp_Venta_Pagar",
                parametros,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}