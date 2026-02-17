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
    public class MVDepartamento : MVBase
    {

        private Departamento _departamentoSeleccionado;

        private DepartamentoRepository _departamentoRepository;
        private UsuarioRepository _usuariorepository;


        private List<Departamento> _listadepartamento;
        private List<Usuario> _listausuario;



        public List<Departamento> listadepartamento => _listadepartamento;
        public List<Usuario> listausuario => _listausuario; 


        public MVDepartamento(
            DepartamentoRepository departamentoRepository,
            UsuarioRepository usuarioRepository
            )
        {
            _departamentoRepository = departamentoRepository;
            _usuariorepository = usuarioRepository;

        }

        public 


        public async Task Inicializa()
        {
            try
            {
                _listausuario = await _usuariorepository.GetAllAsync();
                _listadepartamento = await _departamentoRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("ADMINISTRADOS DE ESPACIOS", "Error al cargar los espacios: " + ex.Message);
            }
        }
    }
}
