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
    /// Interaction logic for ListadoModelosArticulos.xaml
    /// </summary>
    public partial class UCListadoModelos : UserControl
    {
        private MVArticulo _mvArticulo;
        private readonly IServiceProvider _serviceProvider;
        private DialogoModeloArticulo _dialogoModeloArticulo;
        public UCListadoModelos(MVArticulo mvArticulo, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _mvArticulo = mvArticulo;
            _serviceProvider = serviceProvider;
        }


        private async void ucListadoModelos_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvArticulo.Inicializa();
            this.DataContext = _mvArticulo;
        }

        private async void miEditarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {
            _dialogoModeloArticulo = _serviceProvider.GetRequiredService<DialogoModeloArticulo>();
            await _dialogoModeloArticulo.Inicializa(_mvArticulo.modeloArticulo);
            _dialogoModeloArticulo.ShowDialog();
            if (_dialogoModeloArticulo.DialogResult == true)
            {
                // Refrescar la lista de modelos de artículos después de editar
                _mvArticulo.listaModelosArticulos.Refresh();
            }

        }
        

        private void miBorrarModeloArticulo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textoNombreModelo_TextChanged(object sender, TextChangedEventArgs e)
        {
            _mvArticulo.Filtrar();
        }

        private void tipoarticulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           _mvArticulo.Filtrar();
        }
    }

}
