using di.proyecto.clase._2025.Frontend_visual_.ControlUsuarios;
using di.proyecto.clase._2025.Frontend_visual_.Dialogo;
using Fluent;
using MaterialDesignThemes.Wpf;
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
        private DialogoModeloArticulo _dialogoModeloArticulo;
        private DialogoArticulo _dialogoArticulo;
        private readonly IServiceProvider _serviceProvider;
        private DialogoUsuario _dialogoUsuario;

        public MainWindow(DialogoModeloArticulo dialogoModeloArticulo,
                          DialogoArticulo dialogoArticulo,
                          DialogoUsuario dialogoUsuario,
                            IServiceProvider serviceProvider
                          )
        {
            InitializeComponent();
            _dialogoModeloArticulo = dialogoModeloArticulo;
            _dialogoArticulo = dialogoArticulo;
            _dialogoUsuario = dialogoUsuario;
            _serviceProvider = serviceProvider;
        }
        private void btnAddArticulo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoArticulo.ShowDialog();
            _dialogoArticulo = _serviceProvider.GetService(typeof(DialogoArticulo)) as DialogoArticulo; 
        }

        private void btnAddModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoModeloArticulo.ShowDialog();
            _dialogoModeloArticulo = _serviceProvider.GetService(typeof(DialogoModeloArticulo)) as DialogoModeloArticulo;
        }

        private void btnAddUsuario_Click(object sender, RoutedEventArgs e)
        {
          
            _dialogoUsuario.ShowDialog();
            _dialogoUsuario = _serviceProvider.GetService(typeof(DialogoUsuario)) as DialogoUsuario;
        }
    }
}