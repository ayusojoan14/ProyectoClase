using di.proyecto.clase._2025.Frontend_visual_.ControlUsuarios;
using di.proyecto.clase._2025.Frontend_visual_.Dialogo;
using Fluent;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase._2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private DialogoArticulo _dialogoArticulo;
        private DialogoModeloArticulo _dialogoModeloArticulo;
        private readonly IServiceProvider _serviceProvider;
        private DialogoUsuario _dialogoUsuario;
        private UCListadoModelos _ucListadoModelos;
        private UCListadoArticulos _ucListadoArticulos;
        private UCListadoUsuarios _ucListadoUsuarios;
        //serviceprovider se encarga de crear los new automaticamente
        public MainWindow(DialogoModeloArticulo dialogoModeloArticulo,
                          DialogoArticulo dialogoArticulo,
                          IServiceProvider serviceProvider,
                          DialogoUsuario dialogoUsuario,
                          UCListadoModelos ucListadoModelos,
                          UCListadoArticulos ucListadoArticulos,
                          UCListadoUsuarios ucListadoUsuarios)
        {
            InitializeComponent();
            _dialogoArticulo = dialogoArticulo;
            _dialogoModeloArticulo = dialogoModeloArticulo;
            _dialogoUsuario = dialogoUsuario;
            _serviceProvider = serviceProvider;
            _ucListadoModelos = ucListadoModelos;
            _ucListadoArticulos = ucListadoArticulos;
            _ucListadoUsuarios = ucListadoUsuarios;

        }

        private void btnAddModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoModeloArticulo.ShowDialog();
            _dialogoModeloArticulo = _serviceProvider.GetRequiredService<DialogoModeloArticulo>();

        }

        private void btnAddArticulo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoArticulo.ShowDialog();
            _dialogoArticulo = _serviceProvider.GetRequiredService<DialogoArticulo>();
        }

        private void btnAddUsuario_Click(object sender, RoutedEventArgs e)
        {
            _dialogoUsuario.ShowDialog();
            _dialogoUsuario = _serviceProvider.GetRequiredService<DialogoUsuario>();
        }

        private void UCModelo_Click(object sender, RoutedEventArgs e)
        {
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(_ucListadoModelos);
        }
        private void UCArticulo_Click(object sender, RoutedEventArgs e)
        {
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(_ucListadoArticulos);
        }
        private void UCUsuario_Click(object sender, RoutedEventArgs e)
        {
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(_ucListadoUsuarios);
        }
    }
}