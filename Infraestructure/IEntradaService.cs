using Sistema_Almacen_MariaDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Almacen_MariaDB.Infraestructure
{
    public interface IEntradaService
    {
        bool ActualizarEntradasyDetalles(int idEntrada, GetEntradasDto dto);
        GetEntradasDto ObtenerEntradaPorId(int idEntrada, int idSede);
        List<GetEntradasDto> ObtenerEntradasFiltradas(int? idSede = null, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        List<GetEntradasDto> ObtenerEntradasFiltradas(DateTime? fechaInicio, DateTime? fechaFin, int? folioInicio, int? folioFin, int? idProveedor, int? idArticulo, int? idSede);
        List<GetDetallesEntradasDto> ObtenerEntradasPorArticulo(DateTime? fechaInicio, DateTime? fechaFin, int? folioInicio, int? folioFin, int? idArticulo, int? idSede);


        List<GetEntradasDto> ObtenerEntradasPorMovimiento(int? idMovimiento, DateTime? fechaInicio, DateTime? fechaFin, int? idSede = null);
        List<GetEntradasDto> ObtenerEntradasPorProveedor(int idProveedor, DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idSede = null);
        List<GetEntradasDto> ObtenerEntradasPorProveedorYArticulo(int? idSede, int? idProveedor, DateTime? fechaInicio, DateTime? fechaFin, int? folioInicio, int? folioFin);
        List<GetEntradasDto> ObtenerEntradasPorSede(int idSede);
        bool RegistrarEntradayDetalles(EntradasDto entradasdto);
    }
}
