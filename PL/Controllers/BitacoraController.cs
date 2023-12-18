using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class BitacoraController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Bitacora.GetAll();
            ML.Bitacora bitacora = new ML.Bitacora();
            bitacora.Registros = result.Objects;
            return View(bitacora);
        }
    }
}