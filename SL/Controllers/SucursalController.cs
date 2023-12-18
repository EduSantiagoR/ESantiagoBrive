using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/sucursal")]
    public class SucursalController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Sucursal.GetAll();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idSucursal}")]
        [HttpGet]
        public IHttpActionResult GetById(int idSucursal)
        {
            ML.Result result = BL.Sucursal.GetById(idSucursal);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
        [Route("{idSucursal}/{idUsuario}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idSucursal, int idUsuario)
        {
            ML.Result result = BL.Sucursal.Delete(idUsuario, idSucursal);
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
        public IHttpActionResult Add(int idUsuario, [FromBody] ML.Sucursal sucursal)
        {
            ML.Result result = BL.Sucursal.Add(idUsuario, sucursal);
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
        public IHttpActionResult Update(int idUsuario, [FromBody] ML.Sucursal sucursal)
        {
            ML.Result result = BL.Sucursal.Update(idUsuario, sucursal);
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
