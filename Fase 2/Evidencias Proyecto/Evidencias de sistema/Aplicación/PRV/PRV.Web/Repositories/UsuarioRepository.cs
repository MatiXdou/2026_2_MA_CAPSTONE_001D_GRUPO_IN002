using Dapper;
using Microsoft.Data.SqlClient;
using PRV.Web.Models;
using System.Data;

namespace PRV.Web.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PRV_BD")!;
        }

        public void Crear(Usuario usuario)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            conexion.Execute(
                "dbo.sp_Usuario_Crear",
                new
                {
                    usuario.Nombre,
                    usuario.Email,
                    usuario.Rut,
                    usuario.Telefono,
                    usuario.Direccion,
                    usuario.PasswordHash,
                    usuario.TipoCliente,
                    usuario.Estado,
                    usuario.IdEmpresa,
                    usuario.IdRol
                },
                commandType: CommandType.StoredProcedure
            );
        }


        public Usuario BuscarPorEmailEmpresa(string email, long idEmpresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            return conexion.QueryFirstOrDefault<Usuario>("dbo.sp_Usuario_BuscarPorEmailEmpresa",
                new
                {
                    Email = email,
                    IdEmpresa = idEmpresa
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public Usuario ValidarClave(string email, long idEmpresa, string passwordHash)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            return conexion.QueryFirstOrDefault<Usuario>(
                "dbo.sp_Usuario_ValidarClave",
                new
                {
                    Email = email,
                    IdEmpresa = idEmpresa,
                    PasswordHash = passwordHash
                },
                commandType: CommandType.StoredProcedure
            );
        }


    }
}