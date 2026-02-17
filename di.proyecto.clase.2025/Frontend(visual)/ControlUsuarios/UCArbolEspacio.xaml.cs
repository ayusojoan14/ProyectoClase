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
    /// Lógica de interacción para UCArbolEspacio.xaml
    /// </summary>
    public partial class UCArbolEspacio : UserControl
    {
        private MVEspacio _mvEspacio;
        

        public UCArbolEspacio(MVEspacio mvEspacio)
        {
            InitializeComponent();
            _mvEspacio = mvEspacio;
           
        }

        private async void ucArbolEspacio_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvEspacio.Inicializa();
            DataContext = _mvEspacio;
            // Aquí puedes enlazar la lista de espacios a un control de árbol o lista
            // Por ejemplo, si tienes un TreeView llamado treeViewEspacios:
            // treeViewEspacios.ItemsSource = _mvEspacio.listaespacios;
        }
        private void treeViewArbol_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeEspacios.SelectedItem is Espacio )
            {
               dgArticulosPorEspacio.ItemsSource = ((Espacio)treeEspacios.SelectedItem).Articulos;
            }
        }
    }
}
