using Sistema_Almacen_MariaDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Almacen_MariaDB.Infraestructure
{
    interface IReporteService
    {
        byte[] GenerarReportePersonal(List<PersonalDto> personal);
      
      
        byte[] GenerarReportePorProveedorYArticulo(List<GetEntradasDto> entradas, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        byte[] GenerarReporteSedes(List<SedesDto> sedes);
        // byte[] GenerarReporteSumarizado(List<GetEntradasDto> entradas);
     
        byte[] GenerarReporteUnidadesdemedida(List<UnidadesMedidaDto> unidad_De_Medidas);
    }
}
