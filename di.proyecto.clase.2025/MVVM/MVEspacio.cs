using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios_Repositorio_;
using di.proyecto.clase._2025.Frontend.Mensajes;
using di.proyecto.clase._2025.Frontend.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2025.MVVM
{
    public class MVEspacio : MVBase
    {
        private EspacioRepository _espaciorepository;
        private List<Espacio> _listaespacios;
        public List<Espacio> listaespacios => _listaespacios;

        public MVEspacio(EspacioRepository espacioRepository)
        {
            _espaciorepository = espacioRepository;
        }
        public async Task Inicializa()
        {
            try
            {
                _listaespacios = await _espaciorepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("ADMINISTRADOS DE ESPACIOS", "Error al cargar los espacios: " + ex.Message);
            }



        }
    }
}
