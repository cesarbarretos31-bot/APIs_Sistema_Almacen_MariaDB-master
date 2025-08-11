﻿using MySql.Data.MySqlClient;
using Sistema_Almacen_MariaDB.Infraestructure;
using Sistema_Almacen_MariaDB.Models;
using Sistema_Almacen_MariaDB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sistema_Almacen_MariaDB.Controllers
{
    public class InventarioController : ApiController
    {
        private readonly IInventarioService _inventarioService;

        public InventarioController()
        {
            _inventarioService = new InventarioService();
        }

        #region Agregar Articulo a Inventario
        [HttpPost]
        [Route("api/inventario/add")]

        public IHttpActionResult AgregarArticuloaInventario(AgregarArticuloaInventario invArt)
        {
            if (invArt == null || invArt.ID_Sede <= 0)
                return BadRequest("El articulo debe tener una sede valida");
            try
            {
                _inventarioService.AgregarArticuloaInventario(invArt);
                return Ok("Articulo Agregado al inventario exitosamente!");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Obtener articulos por inventario y sede
        [HttpGet]
        [Route("sede/{idSede:int}")]
        public IHttpActionResult GetInventarioPorSede(int idSede)
        {
            try
            {
                var inventario = _inventarioService.GetInventarioPorSede(idSede);
                return Ok(inventario);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Editar Inventario de Articulo
        [HttpPut]
        [Route("editar/{id}")]
        public IHttpActionResult EditarArticuloInventario(int idInv, InventarioDatos inventario)
        {
            try
            {
                if (inventario == null)
                    return BadRequest("Los datos del inventario son requeridos.");

                _inventarioService.EditarArticuloInventario(idInv, inventario);
                return Ok("Inventario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al editar inventario: {ex.Message}");
            }
        }
        #endregion

        #region Actualizar Stock
        [HttpPost]
        [Route("api/inventario/actualizar-stock")]
        public IHttpActionResult ActualizarStock([FromBody] StockEntrada entrada)
        {
            try
            {
                if (entrada == null)
                    return BadRequest("Datos inválidos.");

                var inventarioRepo = new InventarioService();

                bool actualizado = inventarioRepo.ActualizarStockArticulo(entrada);

                if (actualizado)
                    return Ok("Stock actualizado correctamente.");
                else
                    return BadRequest("No se pudo actualizar el stock.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

       
    }
}
