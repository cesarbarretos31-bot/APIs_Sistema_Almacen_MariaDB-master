using iTextSharp.text;
using iTextSharp.text.pdf;
using Sistema_Almacen_MariaDB.Models;
using Sistema_Almacen_MariaDB.Infraestructure;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System;



namespace Sistema_Almacen_MariaDB.Services
{
    public class ReportesService : IReporteService
    {
        #region Reporte Personal 
        public byte[] GenerarReportePersonal(List<PersonalDto> personal)
        {
            using (var ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var fontSub = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                var fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                // Ruta del logo (ajústala según tu estructura)
                string logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Logo_Thh.png");
                Image logo = Image.GetInstance(logoPath);
                logo.ScaleAbsolute(80f, 80f); // tamaño

                // Encabezado con logo y texto
                PdfPTable encabezado = new PdfPTable(2);
                encabezado.WidthPercentage = 100;
                encabezado.SetWidths(new float[] { 1.2f, 4f });

                // Celda de logo
                PdfPCell celdaLogo = new PdfPCell(logo)
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Rowspan = 3
                };
                encabezado.AddCell(celdaLogo);

                // Celda de texto
                PdfPCell empresa = new PdfPCell(new Phrase("TRANSPORTES HIDRO HIDALGUENSES S.A DE C.V", fontTitulo))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(empresa);

                PdfPCell modulo = new PdfPCell(new Phrase("REPORTE DEl PERSONAL", fontTitulo))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(modulo);

                PdfPCell fecha = new PdfPCell(new Phrase("GENERADO EL: " + System.DateTime.Now.ToShortDateString(), fontSub))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(fecha);

                doc.Add(encabezado);
                doc.Add(new Paragraph("\n"));

                // Tabla de personal
                PdfPTable tabla = new PdfPTable(4);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 1.5f, 3f, 3f, 2f });

                tabla.AddCell(new PdfPCell(new Phrase("ID PERSONAL", fontEncabezado)));
                tabla.AddCell(new PdfPCell(new Phrase("NOMBRE", fontEncabezado)));
                tabla.AddCell(new PdfPCell(new Phrase("APELLIDOS", fontEncabezado)));
                tabla.AddCell(new PdfPCell(new Phrase("ID SEDE", fontEncabezado)));

                foreach (var p in personal)
                {
                    tabla.AddCell(new PdfPCell(new Phrase(p.ID_Personal.ToString(), fontNormal)));
                    tabla.AddCell(new PdfPCell(new Phrase(p.Nombre, fontNormal)));
                    tabla.AddCell(new PdfPCell(new Phrase(p.Apellidos, fontNormal)));
                    tabla.AddCell(new PdfPCell(new Phrase(p.ID_Sede.ToString(), fontNormal)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                doc.Add(tabla);

                // Total registros
                doc.Add(new Paragraph("\n"));
                Paragraph total = new Paragraph("TOTAL DE REGISTROS: " + personal.Count, fontSub);
                total.Alignment = Element.ALIGN_RIGHT;
                doc.Add(total);

                doc.Close();
                return ms.ToArray();
            }
        }
        #endregion
        /// 
        #region Reporte Sedes
        public byte[] GenerarReporteSedes(List<SedesDto> sedes)
        {
            using (var ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var fontSub = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                var fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                // Ruta del logo (ajústala según tu estructura)
                string logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Logo_Thh.png");
                Image logo = Image.GetInstance(logoPath);
                logo.ScaleAbsolute(80f, 80f); // tamaño

                // Encabezado con logo y texto
                PdfPTable encabezado = new PdfPTable(2);
                encabezado.WidthPercentage = 100;
                encabezado.SetWidths(new float[] { 1.2f, 4f });

                // Celda de logo
                PdfPCell celdaLogo = new PdfPCell(logo)
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Rowspan = 3
                };
                encabezado.AddCell(celdaLogo);

                // Celda de texto
                PdfPCell empresa = new PdfPCell(new Phrase("TRANSPORTES HIDRO HIDALGUENSES S.A DE C.V", fontTitulo))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(empresa);

                PdfPCell modulo = new PdfPCell(new Phrase("REPORTE DE SEDES", fontTitulo))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(modulo);

                PdfPCell fecha = new PdfPCell(new Phrase("GENERADO EL: " + System.DateTime.Now.ToShortDateString(), fontSub))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(fecha);

                doc.Add(encabezado);
                doc.Add(new Paragraph("\n"));

                // Tabla de personal
                PdfPTable tabla = new PdfPTable(1);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 1.5f });

                tabla.AddCell(new PdfPCell(new Phrase("NOMBRE DE SEDES", fontEncabezado)));

                foreach (var p in sedes)
                {

                    tabla.AddCell(new PdfPCell(new Phrase(p.Nombre_Sede, fontNormal)));

                }

                doc.Add(tabla);

                // Total registros
                doc.Add(new Paragraph("\n"));
                Paragraph total = new Paragraph("TOTAL DE REGISTROS: " + sedes.Count, fontSub);
                total.Alignment = Element.ALIGN_RIGHT;
                doc.Add(total);

                doc.Close();
                return ms.ToArray();
            }
        }

        #endregion
        ////////////////////////
        ///
        #region Reporte Unidades de Medida
        public byte[] GenerarReporteUnidadesdemedida(List<UnidadesMedidaDto> unidad_De_Medidas)
        {
            using (var ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var fontSub = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                var fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                // Ruta del logo (ajústala según tu estructura)
                string logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Logo_Thh.png");
                Image logo = Image.GetInstance(logoPath);
                logo.ScaleAbsolute(80f, 80f); // tamaño

                // Encabezado con logo y texto
                PdfPTable encabezado = new PdfPTable(2);
                encabezado.WidthPercentage = 100;
                encabezado.SetWidths(new float[] { 1.2f, 4f });

                // Celda de logo
                PdfPCell celdaLogo = new PdfPCell(logo)
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Rowspan = 3
                };
                encabezado.AddCell(celdaLogo);

                // Celda de texto
                PdfPCell empresa = new PdfPCell(new Phrase("TRANSPORTES HIDRO HIDALGUENSES S.A DE C.V", fontTitulo))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(empresa);

                PdfPCell modulo = new PdfPCell(new Phrase("REPORTE DE UNIDADES DE MEDIDA", fontTitulo))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(modulo);

                PdfPCell fecha = new PdfPCell(new Phrase("GENERADO EL: " + System.DateTime.Now.ToShortDateString(), fontSub))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                encabezado.AddCell(fecha);

                doc.Add(encabezado);
                doc.Add(new Paragraph("\n"));

                // Tabla de personal
                PdfPTable tabla = new PdfPTable(1);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 1.5f });

                tabla.AddCell(new PdfPCell(new Phrase("NOMBRE DE UNIDADES DE MEDIDA", fontEncabezado)));

                foreach (var p in unidad_De_Medidas)
                {

                    tabla.AddCell(new PdfPCell(new Phrase(p.Nombre_Unidad, fontNormal)));

                }

                doc.Add(tabla);

                // Total registros
                doc.Add(new Paragraph("\n"));
                Paragraph total = new Paragraph("TOTAL DE REGISTROS: " + unidad_De_Medidas.Count, fontSub);
                total.Alignment = Element.ALIGN_RIGHT;
                doc.Add(total);

                doc.Close();
                return ms.ToArray();
            }
        }

        #endregion
        ///////////////////////
        ////
        //////////////////////////////

        ////////////////////
        #region REPORTE PROVEEDOR Y ARTICULO 
        public byte[] GenerarReportePorProveedorYArticulo(
            List<GetEntradasDto> entradas,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null)
        {
            using (var ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                var fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                var fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                string logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Logo_Thh.png");
                Image logo = Image.GetInstance(logoPath);
                logo.ScaleAbsolute(80f, 80f);

                PdfPTable encabezado = new PdfPTable(2);
                encabezado.WidthPercentage = 100;
                encabezado.SetWidths(new float[] { 1.2f, 4f });

                encabezado.AddCell(new PdfPCell(logo) { Border = Rectangle.NO_BORDER, Rowspan = 3 });
                encabezado.AddCell(new PdfPCell(new Phrase("TRANSPORTES HIDRO HIDALGUENSES S.A DE C.V", fontTitulo)) { Border = Rectangle.NO_BORDER });
                encabezado.AddCell(new PdfPCell(new Phrase("REPORTE DE ENTRADAS POR PROVEEDOR Y ARTÍCULO", fontTitulo)) { Border = Rectangle.NO_BORDER });

                string fechaGeneracion = "GENERADO EL: " + DateTime.Now.ToString("dd/MM/yyyy");
                if (fechaInicio.HasValue && fechaFin.HasValue)
                {
                    fechaGeneracion += $" | PERIODO: {fechaInicio.Value:dd/MM/yyyy} - {fechaFin.Value:dd/MM/yyyy}";
                }
                encabezado.AddCell(new PdfPCell(new Phrase(fechaGeneracion, fontNormal)) { Border = Rectangle.NO_BORDER });

                doc.Add(encabezado);
                doc.Add(new Paragraph("\n"));

                // Agrupar por proveedor
                var proveedores = entradas
                    .Where(e => !string.IsNullOrEmpty(e.Razon_Social))
                    .GroupBy(e => e.Razon_Social)
                    .OrderBy(g => g.Key);

                foreach (var grupoProveedor in proveedores)
                {
                    doc.Add(new Paragraph($"PROVEEDOR: {grupoProveedor.Key}", fontEncabezado));

                    PdfPTable tabla = new PdfPTable(8);
                    tabla.WidthPercentage = 100;
                    tabla.SetWidths(new float[] { 1f, 1.5f, 1.5f, 2.5f, 3f, 1f, 1.5f, 1.5f });

                    tabla.AddCell(new Phrase("FOLIO", fontEncabezado));
                    tabla.AddCell(new Phrase("FECHA", fontEncabezado));
                    tabla.AddCell(new Phrase("HORA", fontEncabezado));
                    tabla.AddCell(new Phrase("PROVEEDOR", fontEncabezado));
                    tabla.AddCell(new Phrase("ARTÍCULO", fontEncabezado));
                    tabla.AddCell(new Phrase("CANT", fontEncabezado));
                    tabla.AddCell(new Phrase("P. UNIT", fontEncabezado));
                    tabla.AddCell(new Phrase("TOTAL", fontEncabezado));

                    int totalCantidad = 0;
                    decimal totalImporte = 0;

                    foreach (var entrada in grupoProveedor)
                    {
                        foreach (var detalle in entrada.Detalles)
                        {
                            tabla.AddCell(new Phrase(entrada.ID_Entradas.ToString(), fontNormal));
                            tabla.AddCell(new Phrase(entrada.Fecha?.ToString("dd/MM/yyyy") ?? "-", fontNormal));
                            tabla.AddCell(new Phrase(entrada.Hora?.ToString(@"hh\:mm") ?? "-", fontNormal));
                            tabla.AddCell(new Phrase(entrada.Razon_Social ?? "-", fontNormal));
                            tabla.AddCell(new Phrase(detalle.Nombre_Articulo ?? "-", fontNormal));

                            PdfPCell celdaCant = new PdfPCell(new Phrase(detalle.Cantidad?.ToString() ?? "0", fontNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT };
                            PdfPCell celdaPU = new PdfPCell(new Phrase(detalle.Precio_Unitario?.ToString("C2") ?? "$0.00", fontNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT };
                            PdfPCell celdaTotal = new PdfPCell(new Phrase(detalle.Total?.ToString("C2") ?? "$0.00", fontNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT };

                            tabla.AddCell(celdaCant);
                            tabla.AddCell(celdaPU);
                            tabla.AddCell(celdaTotal);

                            totalCantidad += detalle.Cantidad ?? 0;
                            totalImporte += detalle.Total ?? 0;
                        }
                    }

                    doc.Add(tabla);

                    Paragraph subtotal = new Paragraph($"Subtotal - Cantidad: {totalCantidad} | Total: {totalImporte:C2}", fontNormal);
                    subtotal.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(subtotal);
                    doc.Add(new Paragraph("\n"));
                }

                doc.Close();
                return ms.ToArray();
            }
        }

        public byte[] GenerarReportePorProveedorYArticulo(List<GetEntradasDto> entradas, DateTime? fechaInicio = null, DateTime? fechaFin = null, string nombreProveedor = null)
        {
            throw new NotImplementedException();
        }


        #endregion
        ////
    }
}
       

