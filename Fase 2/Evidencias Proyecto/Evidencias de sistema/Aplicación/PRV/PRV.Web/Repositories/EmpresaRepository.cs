using Dapper;
using Microsoft.Data.SqlClient;
using PRV.Web.Models;
using System.Data;

namespace PRV.Web.Repositories
{
    public class EmpresaRepository
    {
        private readonly string _connectionString;

        public EmpresaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PRV_BD")!;
        }


        // ==========================================
        // LISTAR EMPRESAS
        // ==========================================
        public List<Empresa> Listar()
        {
            using var conexion =
                new SqlConnection(_connectionString);

            var empresas = conexion.Query<Empresa>(
                "dbo.sp_Empresa_Listar",
                commandType: CommandType.StoredProcedure
            ).ToList();

            return empresas;
        }


        // ==========================================
        // EDITAR EMPRESA
        // ==========================================
        public void Editar(Empresa empresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            conexion.Execute(
                "dbo.sp_Empresa_Editar",
                new
                {
                    empresa.IdEmpresa,
                    empresa.Rut,
                    empresa.RazonSocial,
                    empresa.EmailCorporativo,
                    empresa.Telefono,
                    empresa.Direccion,
                    empresa.Estado,
                    empresa.NombreAdministrador,
                    empresa.EmailAdministrador
                },
                commandType: CommandType.StoredProcedure
            );
        }

        // ==========================================
        // CREAR EMPRESA
        // ==========================================
        public void Crear(Empresa empresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            conexion.Execute(
                "dbo.sp_Empresa_Crear",
                new
                {
                    empresa.Rut,
                    empresa.RazonSocial,
                    empresa.EmailCorporativo,
                    empresa.Telefono,
                    empresa.Direccion,
                    empresa.Estado,
                    empresa.NombreAdministrador,
                    empresa.EmailAdministrador

                },
                commandType: CommandType.StoredProcedure
            );
        }


        // ==========================================
        // ELIMINAR EMPRESA
        // ==========================================
        public void Eliminar(long idEmpresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            conexion.Execute(
                "dbo.sp_Empresa_Eliminar",
                new
                {
                    IdEmpresa = idEmpresa
                },
                commandType: CommandType.StoredProcedure
            );
        }

    }
}