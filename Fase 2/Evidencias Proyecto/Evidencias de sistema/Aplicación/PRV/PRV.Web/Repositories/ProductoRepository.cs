using Dapper;
using Microsoft.Data.SqlClient;
using PRV.Web.Models;
using System.Data;

namespace PRV.Web.Repositories
{
    public class ProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PRV_BD")!;
        }


        // ==========================================
        // LISTAR PRODUCTOS POR EMPRESA
        // Catálogo y compra
        // ==========================================
        public List<Producto> ListarPorEmpresa(long idEmpresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            var productos = conexion.Query<Producto>(
                "dbo.sp_Producto_ListarPorEmpresa",
                new
                {
                    IdEmpresa = idEmpresa
                },
                commandType: CommandType.StoredProcedure
            ).ToList();

            return productos;
        }


        // ==========================================
        // LISTAR PRODUCTOS PARA MANTENEDOR
        // Activos e inactivos
        // ==========================================
        public List<Producto> ListarMantenedor(long idEmpresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            var productos = conexion.Query<Producto>(
                "dbo.sp_Producto_ListarMantenedor",
                new
                {
                    IdEmpresa = idEmpresa
                },
                commandType: CommandType.StoredProcedure
            ).ToList();

            return productos;
        }


        // ==========================================
        // CREAR PRODUCTO
        // ==========================================
        public long Crear(Producto producto)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            var idProducto = conexion.QuerySingle<long>(
                "dbo.sp_Producto_Crear",
                new
                {
                    producto.Nombre,
                    producto.Descripcion,
                    producto.PrecioVenta,
                    producto.PrecioMayorista,
                    producto.Stock,
                    producto.Estado,
                    producto.IdEmpresa
                },
                commandType: CommandType.StoredProcedure
            );

            return idProducto;
        }


        // ==========================================
        // EDITAR PRODUCTO
        // ==========================================
        public void Editar(Producto producto)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            conexion.Execute(
                "dbo.sp_Producto_Editar",
                new
                {
                    producto.IdProducto,
                    producto.Nombre,
                    producto.Descripcion,
                    producto.PrecioVenta,
                    producto.PrecioMayorista,
                    producto.Stock,
                    producto.Estado,
                    producto.IdEmpresa
                },
                commandType: CommandType.StoredProcedure
            );
        }


    }
}