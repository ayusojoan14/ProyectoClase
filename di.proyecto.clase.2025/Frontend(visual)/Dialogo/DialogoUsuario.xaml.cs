using di.proyecto.clase._2025.Backend.Modelos;
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
using System.Windows.Shapes;

namespace di.proyecto.clase._2025.Frontend_visual_.Dialogo
{
    /// <summary>
    /// Interaction logic for DialogoUsuario.xaml
    /// </summary>
    public partial class DialogoUsuario : MetroWindow
    {
        private MVArticulo _mvArticulo;
        public DialogoUsuario(MVArticulo mVArticulo)
        {
            InitializeComponent();
            _mvArticulo = mVArticulo;
        }
    }
}
