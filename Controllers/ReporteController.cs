using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

using Sistema_Almacen_MariaDB.Infraestructure;
using Sistema_Almacen_MariaDB.Service;
using Sistema_Almacen_MariaDB.Services;
using System;

namespace Sistema_Almacen_MariaDB.Controllers
{
    public class ReporteController : ApiController
    {
        private readonly IPersonalService _personalService;
        private readonly IReporteService _reporteService;
        private readonly ISedesService _sedeService;
        private readonly IUnidadesMedidaService _unidad_De_MedidaService;
        private readonly IEntradaService _entradaService;
        private readonly IProveedoresService _proveedorService;
        public ReporteController()
        {
            _personalService = new PersonalService();
            _reporteService = new ReportesService();
            _sedeService = new SedesService();
            _unidad_De_MedidaService = new UnidadesMedidaService();
            _entradaService = new EntradaService();
            _proveedorService = new ProveedoresService();

        }
        #region Reporte Personal

        [HttpGet]
        [Route("api/reporte/personal")]
        public HttpResponseMessage DescargarReportePersonal()
        {
            var personal = _personalService.GetAllPersonal();
            var pdfBytes = _reporteService.GenerarReportePersonal(personal);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "ReportePersonal.pdf"
            };

            return response;
        }
        #endregion
        ////////////////////////   
        #region Reporte por sede 
        [HttpGet]
        [Route("api/reporte/sedes")]
        public HttpResponseMessage DescargarReporteSedes()
        {
            var sedes = _sedeService.GetAllSedes();
            var pdfBytes = _reporteService.GenerarReporteSedes(sedes);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "ReporteSedes.pdf"
            };

            return response;
        }
        #endregion
        ////////////////
        #region Reporte Unidades de Medida
        [HttpGet]
        [Route("api/reporte/unidaddemedida")]
        public HttpResponseMessage DescargarReporteUnidadesdemedida()
        {
            var unidad_De_Medidas = _unidad_De_MedidaService.GetAllUMedida();
            var pdfBytes = _reporteService.GenerarReporteUnidadesdemedida(unidad_De_Medidas);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(pdfBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "ReporteUnidadesDeMedida.pdf"
            };

            return response;
        }

        #endregion
        

    }
}
