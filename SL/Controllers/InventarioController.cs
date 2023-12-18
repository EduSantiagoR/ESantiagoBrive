using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/inventario")]
    public class InventarioController : ApiController
    {
        [Route("{idProducto}")]
        [HttpGet]
        public IHttpActionResult GetAll(int idProducto)
        {
            ML.Result result = BL.Inventario.GetAll(idProducto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("asignados/{idSucursal}")]
        [HttpGet]
        //Productos asignados
        public IHttpActionResult GetProductosAsignados(int idSucursal)
        {
            ML.Result result = BL.Inventario.GetProductosAsignados(idSucursal);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("noasignados/{idSucursal}")]
        [HttpGet]
        //Productos NO asignados
        public IHttpActionResult GetProductosNoAsignados(int idSucursal)
        {
            ML.Result result = BL.Inventario.GetProductosNoAsignados(idSucursal);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idUsuario}/{idSucursal}/{idProducto}")]
        [HttpPost]
        public IHttpActionResult Add(int idUsuario, int idSucursal, int idProducto, [FromBody]int stock)
        {
            ML.Result result = BL.Inventario.Add(idUsuario, idSucursal, idProducto, stock);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idUsuario}/{idSucursal}/{idProducto}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idUsuario, int idSucursal, int idProducto)
        {
            ML.Result result = BL.Inventario.Delete(idUsuario, idSucursal, idProducto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idUsuario}/{idSucursal}/{idProducto}")]
        [HttpPut]
        public IHttpActionResult Update(int idUsuario, int idSucursal, int idProducto, [FromBody] int stock)
        {
            ML.Result result = BL.Inventario.Update(idUsuario, idSucursal, idProducto, stock);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}