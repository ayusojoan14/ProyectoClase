using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using di.proyecto.clase._2025.Frontend.Mensajes;
using di.proyecto.clase._2025.Frontend.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2025.MVVM
{
    public class MVUsuario : MVBase
    {
        private Usuario _usuario;

        private UsuarioRepository _usuarioRepository;
        private TipoUsuarioRepository _tipoUsuarioRepository;
        private RolRepository _rolRepository;
        private DepartamentoRepository _departamentoRepository;

        private List<Tipousuario> _listaTiposUsuarios;
        private List<Rol> _listaRoles;
        private List<Departamento> _listaDepartamentos;
        private List<Usuario> _listaUsuarios;

        public Usuario usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        public List<Tipousuario> listaTiposUsuarios => _listaTiposUsuarios;
        public List<Rol> listaRoles => _listaRoles;
        public List<Departamento> listaDepartamentos => _listaDepartamentos;
        public List<Usuario> listaUsuarios => _listaUsuarios;

        public MVUsuario(
            UsuarioRepository usuarioRepository,
            TipoUsuarioRepository tipoUsuarioRepository,
            RolRepository rolRepository,
            DepartamentoRepository departamentoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _rolRepository = rolRepository;
            _departamentoRepository = departamentoRepository;

            _usuario = new Usuario();
        }

        public async Task Inicializa()
        {
            try
            {
                _listaDepartamentos = await GetAllAsync<Departamento>(_departamentoRepository);
                _listaTiposUsuarios = await GetAllAsync<Tipousuario>(_tipoUsuarioRepository);
                _listaRoles = await GetAllAsync<Rol>(_rolRepository);
                _listaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
            }
            catch
            {
                MensajeError.Mostrar(
                    "GESTIÓN USUARIOS",
                    "Error al cargar datos de usuario",
                    0);
            }
        }

        public async Task<bool> GuardarUsuarioAsync(string password)
        {
            bool correcto = true;

            try
            {
                // VALIDACIONES CLAVE
                if (string.IsNullOrWhiteSpace(usuario.Username))
                    return false;

                if (string.IsNullOrWhiteSpace(password))
                    return false;

                if (usuario.TipoNavigation == null || usuario.RolNavigation == null)
                    return false;

                // ASIGNACIONES
                usuario.Password = password;
                usuario.Tipo = usuario.TipoNavigation.Idtipousuario;
                usuario.Rol = usuario.RolNavigation.Idrol;

                if (usuario.DepartamentoNavigation != null)
                    usuario.Departamento = usuario.DepartamentoNavigation.Iddepartamento;

                if (usuario.Idusuario == 0)
                    await _usuarioRepository.AddAsync(usuario);
                else
                    await _usuarioRepository.UpdateAsync(usuario);
            }
            catch
            {
                correcto = false;
            }

            return correcto;
        }
    }


}
