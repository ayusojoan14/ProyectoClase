using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Frontend_visual_.Dialogo;
using di.proyecto.clase._2025.MVVM;
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
    public partial class UCListadoUsuarios : UserControl
    {
        private MVUsuario _mvUsuario;
        public UCListadoUsuarios(MVUsuario mvUsuario)
        {
            InitializeComponent();
            _mvUsuario = mvUsuario;
        }

        private async void ucListadoUsuarios_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvUsuario.Inicializa();
            this.DataContext = _mvUsuario;

        }
    }
}
