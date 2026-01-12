using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using di.proyecto.clase._2025.Frontend.Mensajes;
using di.proyecto.clase._2025.MVVM;
using MahApps.Metro.Controls;
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

namespace di.proyecto.clase._2025.Frontend_visual_.Dialogo
{
    /// <summary>
    /// Interaction logic for DialogoArticulo.xaml
    /// </summary>
    public partial class DialogoArticulo : MetroWindow
    {
        private MVArticulo _mvArticulo;
        public DialogoArticulo (MVArticulo mVArticulo)
        {
            InitializeComponent();
            _mvArticulo = mVArticulo;

        }

        private async void diagArticulo_Loaded(object sender, RoutedEventArgs e)
        {
            await _mvArticulo.Inicializa();
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(_mvArticulo.OnErrorEvent));
            DataContext = _mvArticulo;
        }

        

        private async void btnGuardarArticulo_Click(object sender, RoutedEventArgs e)
        {
           
            if (!_mvArticulo.IsValid(this))
            {
                MensajeError.Mostrar("Existen errores en el formulario. Por favor, corríjalos antes de guardar.", "Error de validación");

            }
            else if (await _mvArticulo.GuardarArticuloAsync())
                {
                    MensajeInformacion.Mostrar("Modelo de artículo guardado correctamente.", "Éxito");
                    DialogResult = true;
                
            }

                try
            {
              
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCancelarArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
