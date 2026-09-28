using PRV.Web.Models;
using PRV.Web.Repositories;

namespace PRV.Web.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void Crear(Usuario usuario)
        {
            _usuarioRepository.Crear(usuario);
        }

        public Usuario BuscarPorEmailEmpresa(string email, long idEmpresa)
        {
            return _usuarioRepository.BuscarPorEmailEmpresa(email, idEmpresa);
        }

        public Usuario ValidarClave(string email, long idEmpresa, string passwordHash)
        {
            return _usuarioRepository.ValidarClave(email, idEmpresa, passwordHash);
        }

        public Usuario ValidarAccesoAdministracion(string email, string passwordHash)
        {
            return _usuarioRepository.ValidarAccesoAdministracion(email, passwordHash);
        }

 
        public List<Usuario> ListarSolicitantes(long idEmpresa)
        {
            return _usuarioRepository.ListarSolicitantes(idEmpresa);
        }



        public void AprobarMayorista(
         long idUsuario,
         long idEmpresa,
         string passwordHash)
        {
            _usuarioRepository.AprobarMayorista(
                idUsuario,
                idEmpresa,
                passwordHash
            );
        }

        public Usuario BuscarPorId(
            long idUsuario,
            long idEmpresa)
        {
            return _usuarioRepository.BuscarPorId(
                idUsuario,
                idEmpresa
            );
        }

    }
}