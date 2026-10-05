using Dapper;
using Microsoft.Data.SqlClient;
using PRV.Web.Models;
using System.Data;

namespace PRV.Web.Repositories
{
    public class VentaRepository
    {
        private readonly string _connectionString;

        public VentaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PRV_BD")!;
        }


        // ==========================================
        // CONSULTAR VENTAS
        // ==========================================
        public List<Venta> Consultar(
            long idEmpresa,
            DateTime fechaDesde,
            DateTime fechaHasta)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            var ventas = conexion.Query<Venta>(
                "dbo.sp_Venta_Consultar",
                new
                {
                    IdEmpresa = idEmpresa,
                    FechaDesde = fechaDesde,
                    FechaHasta = fechaHasta
                },
                commandType: CommandType.StoredProcedure
            ).ToList();

            return ventas;
        }


        // ==========================================
        // CONSULTAR DETALLE DE VENTA
        // ==========================================
        public List<DetalleVenta> ConsultarDetalle(
            long idVenta,
            long idEmpresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            var detalle = conexion.Query<DetalleVenta>(
                "dbo.sp_Venta_ConsultarDetalle",
                new
                {
                    IdVenta = idVenta,
                    IdEmpresa = idEmpresa
                },
                commandType: CommandType.StoredProcedure
            ).ToList();

            return detalle;
        }


        // ==========================================
        // ANULAR VENTA
        // ==========================================
        public void Anular(long idVenta, long idEmpresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            conexion.Execute(
                "dbo.sp_Venta_Anular",
                new
                {
                    IdVenta = idVenta,
                    IdEmpresa = idEmpresa
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}