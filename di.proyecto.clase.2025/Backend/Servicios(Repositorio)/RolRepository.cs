using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2025.Backend.Servicios_Repositorio_
{
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        public RolRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Rol>> logger) : base(context, logger)
        {
        }
    }

}
