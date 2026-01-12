using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Frontend_visual_.Dialogo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase._2025.Frontend_visual_.ControlUsuarios
{

    /// <summary>
    /// Interaction logic for UCArticulos.xaml
    /// </summary>
    public partial class UCArticulos : UserControl
    {
        private UsuarioRepository _usuarioRepository;
        private DialogoModeloArticulo _dialogoModeloArticulo;
        private DialogoArticulo _dialogoArticulo;
        private Usuario _usuarioLogin;
        private readonly IServiceProvider _serviceProvider;
        public UCArticulos(UsuarioRepository usuarioRepository,
                                DialogoModeloArticulo dialogoModeloArticulo,
                                DialogoArticulo dialogoArticulo,
                                Usuario usuarioLogin,
                                IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _usuarioRepository = usuarioRepository;
            _usuarioLogin = _usuarioRepository.UsuarioLogin;
            _dialogoModeloArticulo = dialogoModeloArticulo;
            _dialogoArticulo = dialogoArticulo;
            _usuarioLogin = usuarioLogin;
            _serviceProvider = serviceProvider;
        }



        private void btnAddArticulo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoModeloArticulo.ShowDialog();
        }

        private void btnAddModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoModeloArticulo = _serviceProvider.GetRequiredService<DialogoModeloArticulo>();
            _dialogoModeloArticulo.ShowDialog();

        }

    }
}
