using Sistema_Almacen_MariaDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Almacen_MariaDB.Infraestructure
{
    interface IReportesEntradaService
    {
        byte[] GenerarReportePorArticulo(List<GetEntradasDto> entradas, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        byte[] GenerarReportePorEntrada(GetEntradasDto entrada);
        byte[] GenerarReportePorMovimiento(List<GetEntradasDto> entradas, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        byte[] GenerarReportePorProveedor(List<GetEntradasDto> entradas, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        byte[] GenerarReporteSumarizado(List<GetEntradasDto> entradas, DateTime? fechaInicio = null, DateTime? fechaFin = null);
    }
}
