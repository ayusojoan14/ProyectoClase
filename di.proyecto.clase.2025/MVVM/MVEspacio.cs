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
    public class MVEspacio : MVBase
    {
        //Repositorios necesarios para cargar los datos de los espacios, usuarios, departamentos y modelos de artículos
        private EspacioRepository _espaciorepository;
        private UsuarioRepository _usuariorepository;
        private ModeloArticuloRepository _Modeloarticulorepository;
        private DepartamentoRepository _departamentorepository;
        private ArticuloRepository _articulorepository;


        //Listas para almacenar los datos cargados desde los repositorios
        private List<Usuario> _listausuarios;
        private List<Departamento> _listadepartamentos;
        private List<Modeloarticulo> _listaModeloArticulo;
        private List<Espacio> _listaespacios;
        private List<string> _listaestado;

        //Variable para almacenar el artículo seleccionado en la interfaz (si es necesario)
        private Articulo _articuloSeleccionado;
        
        public List<Espacio> listaespacios => _listaespacios;
        public List<Usuario> listausuarios => _listausuarios;
        public List<Departamento> listadepartamentos => _listadepartamentos;
        public List<Modeloarticulo> listaModeloArticulo => _listaModeloArticulo;

        public List<string> listaestado => _listaestado;
        public Articulo articuloSeleccionado
        {
            get => _articuloSeleccionado;
            set{ _articuloSeleccionado = value;
                OnPropertyChanged(nameof(articuloSeleccionado)); //Aqui va la pulbica
            }
        }



        public MVEspacio(EspacioRepository espacioRepository,
                         UsuarioRepository usuarioRepository,
                         ModeloArticuloRepository modeloArticuloRepository,
                         DepartamentoRepository departamentoRepository,
                         ArticuloRepository articuloRepository
                    )
        {
            _espaciorepository = espacioRepository;
                _usuariorepository = usuarioRepository;
                _Modeloarticulorepository = modeloArticuloRepository;
                _departamentorepository = departamentoRepository;
                _articulorepository = articuloRepository;

        }
        public async Task Inicializa()
        {
            try
            {
                _listaespacios = await _espaciorepository.GetAllAsync();
                _listausuarios = await _usuariorepository.GetAllAsync();
                _listadepartamentos = await _departamentorepository.GetAllAsync();
                _listaModeloArticulo = await _Modeloarticulorepository.GetAllAsync();
                _listaestado = _articulorepository.GetEstado();

            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("ADMINISTRADOS DE ESPACIOS", "Error al cargar los espacios: " + ex.Message);
            }



        }
    }
}
