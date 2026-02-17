using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using di.proyecto.clase._2025.Frontend_visual_.ControlUsuarios;
using di.proyecto.clase._2025.Frontend_visual_.Dialogo;
using di.proyecto.clase._2025.MVVM;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace di.proyecto.clase._2025
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //inversión de control, este apartado se crea para
        //ahorrar codigo en los .cs del login en este caso, de esta forma
        //no tendremos que poner new...

        private DiinventarioexamenContext _contexto;
        /// Propiedad para almacenar el proveedor de servicios
        private IServiceProvider _serviceProvider;
        /// <summary>
        /// Constructor de la clase App
        /// </summary>
        public App()
        {
            // Configurar el contenedor de inyección de dependencias
            var serviceCollection = new ServiceCollection();
            // Configurar los servicios
            ConfigureServices(serviceCollection);
            // Construir el proveedor de servicios
            _serviceProvider = serviceCollection.BuildServiceProvider();
            _contexto = new DiinventarioexamenContext();
        }

        //Inyecta automaticamente la BBDD 
        private void ConfigureServices(ServiceCollection services)
        {
            // Configurar el contexto de la base de datos
            services.AddDbContext<DiinventarioexamenContext>();
            // Configurar el servicio de logging
            services.AddLogging(static configure => configure.AddConsole());
            // Registrar repositorios genéricos
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Registrar servicios y vistas aquí
            // En primer lugar registramos la ventana principal
            services.AddSingleton<MainWindow>();
            // A continuación, registramos los repositorios específicos
            // Lo hacemos con AddScoped para que se cree una nueva instancia
            // de cada repositorio por cada petición
            // Esto es útil para evitar problemas de concurrencia
            services.AddScoped<IGenericRepository<Tipoarticulo>, TipoArticuloRepository>();
            services.AddScoped<IGenericRepository<Modeloarticulo>, ModeloArticuloRepository>();
            services.AddScoped<IGenericRepository<Articulo>, ArticuloRepository>();
            services.AddScoped<IGenericRepository<Usuario>, UsuarioRepository>();
            services.AddScoped<IGenericRepository<Tipousuario>, TipoUsuarioRepository>();
            services.AddScoped<IGenericRepository<Rol>, RolRepository>();
            services.AddScoped<IGenericRepository<Departamento>, DepartamentoRepository>();
            services.AddScoped<IGenericRepository<Espacio>, EspacioRepository>();
            services.AddScoped<IGenericRepository<Grupo>, GrupoRepository>();
            
            
            // Registramos los servicios específicos
            services.AddScoped<UsuarioRepository>();
            services.AddScoped<ArticuloRepository>();
            services.AddScoped<ModeloArticuloRepository>();
            services.AddScoped<TipoArticuloRepository>();
            services.AddScoped<DepartamentoRepository>();
            services.AddScoped<EspacioRepository>();
            services.AddScoped<RolRepository>();
            services.AddScoped<TipoUsuarioRepository>();
            services.AddScoped<GrupoRepository>();
            
            // Registramos las interfaces de usuario
            services.AddTransient<Login>();
            //services.AddTransient<UCArticulos>();
            services.AddTransient<UCListadoModelos>();
            services.AddTransient<UCListadoArticulos>();
            services.AddTransient<UCListadoUsuarios>();
            services.AddTransient<UCArbolEspacio>();
            services.AddTransient<UCArbolDepartamento>();
            services.AddTransient<DialogoModeloArticulo>();
            services.AddTransient<DialogoArticulo>();
            services.AddTransient<DialogoUsuario>();
            //Registramos los objetos MVVM,
            //el framework recomienda que los MV sean transient.
            services.AddTransient<MVArticulo>();
            services.AddTransient<MVUsuario>();
            services.AddTransient<MVEspacio>();
            services.AddTransient<MVGrupos>();
            services.AddTransient<MVDepartamento>();
        }

        //ESTE CODIGO HACE QUE EL LOGIN SEA LA PRIMERA VENTANA EN ABRIRSE AL EJECUTAR
        protected override void OnStartup(StartupEventArgs e)
        {
            // Se genera la ventana de Login
            //esto es como un new
            var loginWindow = _serviceProvider.GetService<Login>();
            loginWindow.Show();
            base.OnStartup(e);
        }
    }


}




