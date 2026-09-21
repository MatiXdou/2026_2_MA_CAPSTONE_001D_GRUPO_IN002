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
    }
}