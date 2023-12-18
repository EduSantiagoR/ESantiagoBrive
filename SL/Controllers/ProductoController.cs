using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/producto")]
    public class ProductoController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAll();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idProducto}")]
        [HttpGet]
        public IHttpActionResult GetById(int idProducto)
        {
            ML.Result result = BL.Producto.GetById(idProducto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idUsuario}")]
        [HttpPost]
        public IHttpActionResult Add(int idUsuario, [FromBody] ML.Inventario producto)
        {
            ML.Result result = BL.Producto.Add(idUsuario, producto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idUsuario}")]
        [HttpPut]
        public IHttpActionResult Update(int idUsuario, [FromBody] ML.Inventario inventario)
        {
            ML.Result result = BL.Producto.Update(idUsuario, inventario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idUsuario}/{idProducto}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idUsuario, int idProducto)
        {
            ML.Result result = BL.Producto.Delete(idUsuario, idProducto);
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
