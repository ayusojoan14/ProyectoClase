using di.proyecto.clase._2025.Backend.Modelos;
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
    /// Lógica de interacción para UCArbolDepartamento.xaml
    /// </summary>
    public partial class UCArbolDepartamento : UserControl
    {
        private MVDepartamento _mvDepartamento;

        public UCArbolDepartamento(MVDepartamento mvDepartamento)
        {
            InitializeComponent();
            _mvDepartamento = mvDepartamento;
           
        }

        private async void ucArbolDepartamento_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvDepartamento.Inicializa();
            DataContext = _mvDepartamento;
            // Aquí puedes enlazar la lista de espacios a un control de árbol o lista
            // Por ejemplo, si tienes un TreeView llamado treeViewEspacios:
            // treeViewEspacios.ItemsSource = _mvEspacio.listaespacios;
        }

        private void treeDepartamento_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeDepartamento.SelectedItem is Espacio)
            {
                departamento.ItemsSource = ((Espacio)treeDepartamento.SelectedItem).Articulos;
            }
        }
    }
}
