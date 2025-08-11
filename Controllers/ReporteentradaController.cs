using Sistema_Almacen_MariaDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Web;

using Sistema_Almacen_MariaDB.Infraestructure;
using Sistema_Almacen_MariaDB.Service;
using System.Web.Http;
using Sistema_Almacen_MariaDB.Models;


namespace Sistema_Almacen_MariaDB.Controllers
{

    public class ReporteentradaController : ApiController
    {
      
        private readonly IEntradaService _entradaService;
       
        public ReporteentradaController()
        {
        
            _entradaService = new EntradaService();
            

        }
        #region Reporte Entradas Por Fechas #2
        [HttpGet]
        [Route("api/reporteentrada/entradas/fechas")]
        public HttpResponseMessage DescargarReporteSumarizado(int? idSede = null, string fechaInicio = null, string fechaFin = null)
        {
            DateTime? fechaInicioParsed = null;
            DateTime? fechaFinParsed = null;

            if (!string.IsNullOrEmpty(fechaInicio))
                fechaInicioParsed = DateTime.Parse(fechaInicio);

            if (!string.IsNullOrEmpty(fechaFin))
                fechaFinParsed = DateTime.Parse(fechaFin);

            var entradas = _entradaService.ObtenerEntradasFiltradas(idSede, fechaInicioParsed, fechaFinParsed);

            var pdfBytes = new ReporteEntradaService().GenerarReporteSumarizado(entradas, fechaInicioParsed, fechaFinParsed);


            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Reporte_Entradas_Fechas.pdf"
            };

            return response;
        }

        #endregion

        #region Entradas por ID_Entrada #1 
        [HttpGet]
        [Route("api/reporte/entrada/ID")]
        public HttpResponseMessage DescargarReportePorEntrada(int idSede, int idEntrada)
        {
            var entrada = _entradaService.ObtenerEntradaPorId(idEntrada, idSede);

            if (entrada == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("No se encontró la entrada con ese ID y sede.")
                };

            var pdfBytes = new ReporteEntradaService().GenerarReportePorEntrada(entrada);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"Entrada_{idEntrada}.pdf"
            };

            return response;
        }


        #endregion

        #region Entradas Por Proveedor #3
        [HttpGet]
        [Route("api/reporte/entradas/proveedor")]
        public HttpResponseMessage DescargarReporteEntradasPorProveedor(
         int? idProveedor = null,
         string fechaInicio = null,
         string fechaFin = null,
         int? idSede = null) // 👈 nuevo parámetro aquí
        {
            DateTime? fechaInicioParsed = null;
            DateTime? fechaFinParsed = null;

            if (!string.IsNullOrEmpty(fechaInicio))
                fechaInicioParsed = DateTime.Parse(fechaInicio);

            if (!string.IsNullOrEmpty(fechaFin))
                fechaFinParsed = DateTime.Parse(fechaFin);

            // Obtener entradas dependiendo de los filtros
            List<GetEntradasDto> entradas;

            if (idProveedor.HasValue)
            {
                // Si hay proveedor, usa el método actual
                entradas = _entradaService.ObtenerEntradasPorProveedor(idProveedor.Value, fechaInicioParsed, fechaFinParsed, idSede.Value);
            }
            else
            {
                // Si no hay proveedor, pero se puede filtrar por sede
                entradas = _entradaService.ObtenerEntradasFiltradas(idSede, fechaInicioParsed, fechaFinParsed);
            }

            var pdfBytes = new ReporteEntradaService().GenerarReportePorProveedor(entradas, fechaInicioParsed, fechaFinParsed);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = idProveedor.HasValue ? "Entradas_Por_Proveedor.pdf" : "Entradas_Agrupadas_Por_Proveedor.pdf"
            };

            return response;
        }


        #endregion
        #region REPORTE ENTRADAS POR ARTICULO #4
        [HttpGet]
        [Route("api/reporte/entradas/articulo")]
        public HttpResponseMessage DescargarReporteEntradasPorArticulo(
     int? idArticulo = null,
     string fechaInicio = null,
     string fechaFin = null,
     int? idSede = null) // 👈 nuevo parámetro aquí
        {
            DateTime? fechaInicioParsed = null;
            DateTime? fechaFinParsed = null;

            if (!string.IsNullOrEmpty(fechaInicio))
                fechaInicioParsed = DateTime.Parse(fechaInicio);

            if (!string.IsNullOrEmpty(fechaFin))
                fechaFinParsed = DateTime.Parse(fechaFin);

            var entradas = _entradaService.ObtenerEntradasPorArticulo(
                idArticulo,
                fechaInicioParsed,
                fechaFinParsed,
                idSede // 👈 pasamos sede
            );

            var pdfBytes = new ReporteEntradaService()
                .GenerarReportePorArticulo(entradas, fechaInicioParsed, fechaFinParsed);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Reporte_Entradas_Por_Articulo.pdf"
            };

            return response;
        }


        #endregion
        #region ENTRADAS POR MOVIMIENTOS #5
        [HttpGet]
        [Route("api/reporte/entradas/movimiento")]
        public HttpResponseMessage DescargarReporteEntradasPorMovimiento(
    int? idMovimiento = null,
    string fechaInicio = null,
    string fechaFin = null,
    int? idSede = null) // 👈 Nuevo parámetro
        {
            DateTime? fechaInicioParsed = null;
            DateTime? fechaFinParsed = null;

            if (!string.IsNullOrEmpty(fechaInicio))
                fechaInicioParsed = DateTime.Parse(fechaInicio);

            if (!string.IsNullOrEmpty(fechaFin))
                fechaFinParsed = DateTime.Parse(fechaFin);

            var entradas = _entradaService.ObtenerEntradasPorMovimiento(
                idMovimiento,
                fechaInicioParsed,
                fechaFinParsed,
                idSede // 👈 Pasamos sede
            );

            var pdfBytes = new ReporteEntradaService()
                .GenerarReportePorMovimiento(entradas, fechaInicioParsed, fechaFinParsed);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Entradas_Por_Movimiento.pdf"
            };

            return response;
        }


        #endregion
    }
}

