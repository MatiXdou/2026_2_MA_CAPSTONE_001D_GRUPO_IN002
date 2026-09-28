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

  
        public Usuario ValidarAccesoAdministracion(
            string email,
            string passwordHash)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            var usuario = conexion.QueryFirstOrDefault<Usuario>(
                "dbo.sp_Usuario_ValidarAccesoAdministracion",
                new
                {
                    Email = email,
                    PasswordHash = passwordHash
                },
                commandType: CommandType.StoredProcedure
            );

            return usuario;
        }

      
        public List<Usuario> ListarSolicitantes(long idEmpresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            return conexion.Query<Usuario>(
                "dbo.sp_Usuario_ListarSolicitantes",
                new
                {
                    IdEmpresa = idEmpresa
                },
                commandType: CommandType.StoredProcedure
            ).ToList();
        }

        public void AprobarMayorista(
            long idUsuario,
            long idEmpresa,
            string passwordHash)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            conexion.Execute(
                "dbo.sp_Usuario_AprobarMayorista",
                new
                {
                    IdUsuario = idUsuario,
                    IdEmpresa = idEmpresa,
                    PasswordHash = passwordHash
                },
                commandType: CommandType.StoredProcedure
            );
        }


        public Usuario BuscarPorId(
            long idUsuario,
            long idEmpresa)
        {
            using var conexion =
                new SqlConnection(_connectionString);

            return conexion.QueryFirstOrDefault<Usuario>(
                "dbo.sp_Usuario_BuscarPorId",
                new
                {
                    IdUsuario = idUsuario,
                    IdEmpresa = idEmpresa
                },
                commandType: CommandType.StoredProcedure
            );
        }




    }
}