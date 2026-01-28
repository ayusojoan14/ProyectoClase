using di.proyecto.clase._2025.MVVM;
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
    /// Interaction logic for ListadoModelosArticulos.xaml
    /// </summary>
    public partial class ListadoModelosArticulos : UserControl
    {
        private MVArticulo _mvArticulo;
        public ListadoModelosArticulos()
        {
            InitializeComponent();
           
        }

        private void cbTipoArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mvArticulo.Filtrar();
        }
        private void txtNombreModelo_TextChanged(object sender, TextChangedEventArgs e)
        {
            _mvArticulo.Filtrar();
        }
        private void btnLimpiarFiltros_Click(object sender, RoutedEventArgs e)
        {
            _mvArticulo.LimpiarFiltros();
        }
    }
}
