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
    public class MVGrupos : MVBase
    {
        private GrupoRepository _gruporepository;
        private List<Grupo> _listagrupos;
        public List<Grupo> listagrupos => _listagrupos;

        public MVGrupos(GrupoRepository gruporepository)
        {
            _gruporepository = gruporepository;
        }

        public async Task Inicializa()
        {
            try
            {
                _listagrupos = await _gruporepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("ADMINISTRADOS DE GRUPOS", "Error al cargar los grupos: " + ex.Message);
            }
        }
    }
}
