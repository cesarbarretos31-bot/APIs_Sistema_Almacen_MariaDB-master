using iTextSharp.text;
using iTextSharp.text.pdf;
using Sistema_Almacen_MariaDB.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Almacen_MariaDB.Infraestructure
{
    public interface IInventarioService
    {
        bool ActualizarStockArticulo(StockEntrada entrada);
        void AgregarArticuloaInventario(AgregarArticuloaInventario invArt);
        void EditarArticuloInventario(int idInv, InventarioDatos inventario);
       
        List<InventarioArticulos> GetInventarioPorSede(int idSede);
        
        List<InventarioArticulos> ObtenerInventarioPorSede(int idSede);
    }
}
