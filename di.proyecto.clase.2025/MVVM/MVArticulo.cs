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
using System.Windows.Data;

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
        private Usuario _usuario;
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
        private UsuarioRepository _usuarioRepository;
        private DepartamentoRepository _departamentoRepository;
        /// <summary>
        /// lista de tipos de artículos disponibles
        /// </summary>
        private List<Tipoarticulo> _listaTipoArticulos;
        private List<Modeloarticulo> _listaModelosArticulos;
        private List<Articulo> _listaArticulos;
        private List<Usuario> _listaUsuarios;
        private List<Espacio> _listaEspacios;
        private List<Departamento> _listaDepartamentos;



        private List<Predicate<Modeloarticulo>> _criterios;
        private Tipoarticulo _tipoarticuloSeleccionado;
        private Predicate<Modeloarticulo> _criterioTipoArticulo;
        private String _textoNombre;
        private Predicate<Modeloarticulo> _criterioNombreTipo;
        private Predicate<object> _predicadoFiltros;


        #endregion
        #region Getters y Setters
        public List<Tipoarticulo> listaTipoArticulos => _listaTipoArticulos;
        public ListCollectionView listaModelosArticulos { get; set; }
        public List<Usuario> listaUsuarios => _listaUsuarios;
        public ListCollectionView listaArticulos { get; set; }
        public List<Espacio> listaEspacios => _listaEspacios;
        public List<Departamento> listaDepartamentos => _listaDepartamentos;




        //"modeloArticulo" será el nombre que pongamos en el binding para que se guarden los datos
        public Modeloarticulo modeloArticulo
        {
            get => _modeloArticulo;
            set => SetProperty(ref _modeloArticulo, value);
        }
        public Articulo articulo
        {
            get => _articulo;
            set => SetProperty(ref _articulo, value);
        }

        public Usuario usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        public string textoNombre
        {
            get => _textoNombre;
            set => SetProperty(ref _textoNombre, value);
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
            _usuarioRepository = usuarioRepository;
            _departamentoRepository = departamentoRepository;
            //_modeloArticulo = new Modeloarticulo();
            //_articulo = new Articulo();
        }

        public async Task Inicializa()
        {
            try
            {
                await InicializaListas();
                InicializaCriterios();
                _predicadoFiltros = new Predicate<object>(FiltroCriterios);

                //_articulo.Fechaalta = DateTime.Now;
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN ARTÍCULOS", "Error al cargar los tipos de artículos\n" +
                    "No puedo conectar con la base de datos", 0);
            }
        }

        #region Metodos privados
        private void InicializaCriterios()
        {
            _criterioTipoArticulo = new Predicate<Modeloarticulo>(m => m.TipoNavigation != null
                                                            && m.TipoNavigation.Equals(_tipoarticuloSeleccionado));
            _criterioNombreTipo = new Predicate<Modeloarticulo>(m => !string.IsNullOrEmpty(_textoNombre)
                                                            && m.Nombre!.ToLower().StartsWith(_textoNombre.ToLower()));
        }
        private async Task InicializaListas()
        {
            _listaTipoArticulos = await GetAllAsync<Tipoarticulo>(_tipoArticuloRepository);
            _listaDepartamentos = await GetAllAsync<Departamento>(_departamentoRepository);
            _listaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
            _listaEspacios = await GetAllAsync<Espacio>(_espacioRepository);
            _listaModelosArticulos = await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
            _listaArticulos = await GetAllAsync<Articulo>(_articuloRepository);
            listaModelosArticulos = new ListCollectionView(_listaModelosArticulos);
            listaArticulos = new ListCollectionView(_listaArticulos);
            _criterios = new List<Predicate<Modeloarticulo>>();
        }

        private void AddCriterios()
        {
            // Borramos los criterios
            _criterios.Clear();
            // Añadimos los criterios seleccionados
            if (tipoarticuloSeleccionado != null) { _criterios.Add(_criterioTipoArticulo); }
            if (!string.IsNullOrEmpty(textoNombre)) { _criterios.Add(_criterioNombreTipo); }
        }

        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            Modeloarticulo modelo = (Modeloarticulo)item;
            if (_criterios != null)
            {
                correcto = _criterios.TrueForAll(x => x(modelo));
            }
            return correcto;
        }
        public void Filtrar()
        {
            AddCriterios();
            listaModelosArticulos.Filter = _predicadoFiltros;
        }

        public void LimpiarFiltros()
        {
            tipoarticuloSeleccionado = null;
            textoNombre = string.Empty;

            listaModelosArticulos.Filter = null;
        }

        #endregion





        public Tipoarticulo tipoarticuloSeleccionado
        {
            get => _tipoarticuloSeleccionado;
            set => SetProperty(ref _tipoarticuloSeleccionado, value);
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
    }

}
