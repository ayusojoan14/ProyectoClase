using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using di.proyecto.clase._2025.Frontend.Mensajes;
using di.proyecto.clase._2025.Frontend.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace di.proyecto.clase._2025.MVVM
{
    public class MVArticulo : MVBase
    {
        #region Campos y propiedades privadas
        /// <summary>
        /// Objeto que guarda el modelo de artículo actual
        /// Está vinculado a la vista para mostrar y editar los datos del artículo
        /// </summary>
        private Modeloarticulo _modeloArticulo;
        private Articulo _articulo;
        /// <summary>
        /// Repositorio para gestionar las operaciones de datos relacionadas con los modelos de artículo
        /// </summary>
        private ModeloArticuloRepository _modeloArticuloRepository;
        /// <summary>
        /// Repositorio para gestionar las operaciones de datos relacionadas con los tipos de artículo
        /// </summary>
        private TipoArticuloRepository _tipoArticuloRepository;
        private EspacioRepository _espacioRepository;
        private ArticuloRepository _articuloRepository;
        private DepartamentoRepository _departamentoRepository;
        private UsuarioRepository _usuarioRepository;
        /// <summary>
        /// lista de tipos de artículos disponibles
        /// </summary>
        private List<Tipoarticulo> _listaTipoArticulos;
        private List<Modeloarticulo> _listaModelosArticulos;
        private List<Articulo> _listaArticulos;
        private List<Espacio> _listaEspacios;
        private List<Departamento> _listaDepartamentos;
        private List<Usuario> _listaUsuarios;
        private List<Predicate<Modeloarticulo>> _criterios;//Esto es un filtro
        private Tipoarticulo _tipoArticuloSeleccionado;
        private Predicate<Modeloarticulo> _criterioTipoArticulo;
        private Predicate<Object> _predicadoFiltros;
        private String _textoNombre;
        private Predicate<Modeloarticulo> _criterioNombreTipo;

        #endregion
        #region Getters y Setters
        public List<Tipoarticulo> listaTiposArticulos => _listaTipoArticulos;
        public List<Modeloarticulo> listaModelosArticulos => _listaModelosArticulos;
        public List<Articulo> listaArticulos => _listaArticulos;
        public List<Espacio> listaEspacios => _listaEspacios;
        public List<Departamento> listaDepartamentos => _listaDepartamentos;
        public List<Usuario> listaUsuarios => _listaUsuarios;

        //"modeloArticulo" será el nombre que pongamos en el binding para que se guarden los datos
        public Modeloarticulo modeloArticulo
        {
            get => _modeloArticulo;
            set => SetProperty(ref _modeloArticulo, value);
        }

        public String textoNombre
        {
            get => _textoNombre;
            set => SetProperty(ref _textoNombre, value);
        }
        public Articulo articulo
        {
            get => _articulo;
            set => SetProperty(ref _articulo, value);
        }

        public Tipoarticulo tipoArticuloSeleccionado
        {
            get => _tipoArticuloSeleccionado;
            set => SetProperty(ref _tipoArticuloSeleccionado, value);
        }
        #endregion
        // Aquí puedes añadir propiedades y métodos específicos para el ViewModel de Artículo
        public MVArticulo(ModeloArticuloRepository modeloArticuloRepository,
                          TipoArticuloRepository tipoArticuloRepository,
                          ArticuloRepository articuloRepository,
                          EspacioRepository espacioRepository,
                          DepartamentoRepository departamentoRepository,
                          UsuarioRepository usuarioRepository)
        {
            _modeloArticuloRepository = modeloArticuloRepository;
            _tipoArticuloRepository = tipoArticuloRepository;
            _articuloRepository = articuloRepository;
            _espacioRepository = espacioRepository;
            _departamentoRepository = departamentoRepository;
            _usuarioRepository = usuarioRepository;
            //_modeloArticulo = new Modeloarticulo();
            //_articulo = new Articulo();
        }

        public async Task Inicializa()
        {
            try
            {
                InicializaLista();
                InicializaCriterios();
                _predicadoFiltros = new Predicate<object>(FiltroCriterios);
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN ARTÍCULOS", "Error al cargar los tipos de artículos\n" +
                    "No puedo conectar con la base de datos", 0);
            }
        }

        public async Task<bool> GuardarModeloArticuloAsync()
        {
            bool correcto = true;
            try
            {
                if (modeloArticulo.Idmodeloarticulo == 0)
                {
                    // Nuevo modelo de artículo
                    await _modeloArticuloRepository.AddAsync(modeloArticulo);
                }
                else
                {
                    // Actualizar modelo de artículo existente
                    await _modeloArticuloRepository.UpdateAsync(modeloArticulo);
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y la registramos en el log
                correcto = false;
            }
            return correcto;
        }

        public void Filtrar()
        {
            AddCriterios();
            // Aplicamos el filtro a la vista de colección
            listaModelosArticulos.Filter = _predicadoFiltros;
        }

        private async Task<int> ObtenerNuevoIdArticulo()
        {
            try
            {

                List<Articulo> articulos = (List<Articulo>)await _articuloRepository.GetAllAsync();


                int maxCodigo = articulos.Max(e => (int?)e.Idarticulo) ?? 0;

                return maxCodigo + 1;
            }
            catch
            {

                return 1000;
            }
        }

        private async void btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            Articulo articulo = new Articulo();

            articulo.Idarticulo = await ObtenerNuevoIdArticulo();
            RecogeDatos(articulo);

            if (string.IsNullOrEmpty(articulo.Numserie) || string.IsNullOrEmpty(articulo.Estado) || articulo.ModeloNavigation == null)
            {
                MessageBox.Show("Los campos obligatorios no pueden estar vacíos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _articuloRepository.AddAsync(articulo);
                //await _context.SaveChangesAsync();
                MessageBox.Show("Empleado guardado correctamente", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Método privado para recoger los datos del ViewModel y asignarlos al objeto Articulo.
        // Este método debe rellenar las propiedades del objeto Articulo a partir de los datos del ViewModel.
        // Puedes ajustar los campos según los datos que manejes en tu formulario.
        private void RecogeDatos(Articulo articulo)
        {
            articulo.Numserie = _articulo.Numserie;
            articulo.Estado = _articulo.Estado;
            articulo.Fechaalta = _articulo.Fechaalta;
            articulo.Fechabaja = _articulo.Fechabaja;
            articulo.Usuarioalta = _articulo.Usuarioalta;
            articulo.Usuariobaja = _articulo.Usuariobaja;
            articulo.Modelo = _articulo.Modelo;
            articulo.Departamento = _articulo.Departamento;
            articulo.Espacio = _articulo.Espacio;
            articulo.Dentrode = _articulo.Dentrode;
            articulo.Observaciones = _articulo.Observaciones;
            articulo.ModeloNavigation = _articulo.ModeloNavigation;
            articulo.DepartamentoNavigation = _articulo.DepartamentoNavigation;
            articulo.EspacioNavigation = _articulo.EspacioNavigation;
            articulo.DentrodeNavigation = _articulo.DentrodeNavigation;
            articulo.UsuarioaltaNavigation = _articulo.UsuarioaltaNavigation;
            articulo.UsuariobajaNavigation = _articulo.UsuariobajaNavigation;
        }

        public void LimpiarFiltros()
        {
            tipoArticuloSeleccionado = null;
            textoNombre = string.Empty;
            // Refrescamos la vista para eliminar los filtros
            listaModelosArticulos.Filter = null;
        }
        public async Task<bool> GuardarArticuloAsync()
        {
            bool correcto = true;
            try
            {
                if (_articulo.Idarticulo == 0)
                {
                    // Nuevo modelo de artículo
                    await _articuloRepository.AddAsync(articulo);
                }
                else
                {
                    // Actualizar modelo de artículo existente
                    await _articuloRepository.UpdateAsync(articulo);
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y la registramos en el log
                correcto = false;
            }
            return correcto;
        }

        #region Metodos privados

        private void InicializaCriterios()
        {
            _criterioTipoArticulo = new Predicate<Modeloarticulo>(m => m.TipoNavigation != null
                                                                    && m.TipoNavigation.Equals(_tipoArticuloSeleccionado));

            _criterioNombreTipo = new Predicate<Modeloarticulo>(m => !string.IsNullOrEmpty(_textoNombre)
                                                                && m.Nombre!.ToLower().StartsWith(_textoNombre.ToLower()));
        }

        private async Task InicializaLista()
        {
            _listaTipoArticulos = await GetAllAsync<Tipoarticulo>(_tipoArticuloRepository);
            _listaDepartamentos = await GetAllAsync<Departamento>(_departamentoRepository);
            _listaEspacios = await GetAllAsync<Espacio>(_espacioRepository);
            _listaModelosArticulos = await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
            _listaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
            _articulo.Fechaalta = DateTime.Now;
            _criterios = new List<Predicate<Modeloarticulo>>();
        }


        private void AddCriterios()
        {
            _criterios.Clear();

            // Añadimos los criterios según los filtros seleccionados
            if (_tipoArticuloSeleccionado != null)  { _criterios.Add(_criterioTipoArticulo);  }
            if (!string.IsNullOrEmpty(_textoNombre)) { _criterios.Add(_criterioNombreTipo); }

        }

        /// <summary>
        /// 
        private bool FiltroCriterios(object item) {
            bool correcto = true;
            Modeloarticulo modelo = (Modeloarticulo)item; 
            if (_criterios != null) 
            { 
                correcto = _criterios.TrueForAll(x => x(modelo));
            }
            
            return correcto; 
        }
        #endregion

    }
}
