using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class InventarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Inventario inventario = new ML.Inventario();
            inventario.Inventarios = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.GetAsync($"inventario/{0}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach(var itemResult in readTask.Result.Objects)
                    {
                        ML.Inventario registro = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Inventario>(itemResult.ToString());
                        inventario.Inventarios.Add(registro);
                    }
                }
            }
            ML.Result resultProductos = BL.Producto.GetAll();
            inventario.Pruducto = new ML.Producto();
            inventario.Pruducto.Productos = resultProductos.Objects;
            return View(inventario);
        }
        [HttpPost]
        public ActionResult GetAll(int idProducto)
        {
            ML.Result resultProductos = BL.Producto.GetAll();

            ML.Inventario inventario = new ML.Inventario();
            inventario.Inventarios = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.GetAsync($"inventario/{idProducto}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var itemResult in readTask.Result.Objects)
                    {
                        ML.Inventario registro = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Inventario>(itemResult.ToString());
                        inventario.Inventarios.Add(registro);
                    }
                }
            }
            inventario.Pruducto = new ML.Producto();
            inventario.Pruducto.Productos = resultProductos.Objects;
            return View(inventario);
        }
        [HttpGet]
        public ActionResult GetProductosAsignados(int idSucursal)
        {
            Session["IdSucursal"] = idSucursal;
            ML.Inventario inventario = new ML.Inventario();
            inventario.Inventarios = new List<object>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.GetAsync($"inventario/asignados/{idSucursal}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach(var itemResult in readTask.Result.Objects)
                    {
                        ML.Inventario registro = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Inventario>(itemResult.ToString());
                        inventario.Inventarios.Add(registro);
                    }
                }
            }

            return View(inventario);
        }
        [HttpGet]
        public ActionResult GetProductosNoAsignados()
        {
            int idSucursal = int.Parse(Session["IdSucursal"].ToString());

            ML.Inventario productosNoAsignados = new ML.Inventario();
            productosNoAsignados.Pruducto = new ML.Producto();
            productosNoAsignados.Pruducto.Productos = new List<object>();

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.GetAsync($"inventario/noasignados/{idSucursal}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach(var itemResult in readTask.Result.Objects)
                    {
                        ML.Producto producto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(itemResult.ToString());
                        productosNoAsignados.Pruducto.Productos.Add(producto);
                    }
                }
            }
            return View(productosNoAsignados);
        }
        [HttpPost]
        public ActionResult GetProductosNoAsignados(List<int> stocks, List<int> productos)
        {
            int idSucursal = int.Parse(Session["IdSucursal"].ToString());
            int idUsuario = int.Parse(Session["idUsuario"].ToString());

            int index = 0;
            int noAgregados = 0;
            int siAgregados = 0;
            foreach(int idProducto in productos)
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:13046/api/");
                    var taskResponse = client.PostAsJsonAsync($"inventario/{idUsuario}/{idSucursal}/{idProducto}", stocks[index]);
                    taskResponse.Wait();

                    var resultService = taskResponse.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        siAgregados++;
                    }
                    else
                    {
                        noAgregados++;
                    }
                }
                index++;
            }
            if(noAgregados == 0)
            {
                ViewBag.Mensaje = $"Has asignado un total de {siAgregados} productos a la tienda.";
            }
            else
            {
                if(siAgregados > 0)
                {
                    ViewBag.Mensaje = $"Has asignado un total de {siAgregados} a la tienda, pero ha habido problemas para agregar {noAgregados} productos.";
                }
                else
                {
                    ViewBag.Mensaje = $"Ha ocurrido un error al tratar de agregar los productos seleccionados.";
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int idProducto)
        {
            int idSucursal = int.Parse(Session["IdSucursal"].ToString());
            int idUsuario = int.Parse(Session["idUsuario"].ToString());

            ML.Result result = new ML.Result();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.DeleteAsync($"inventario/{idUsuario}/{idSucursal}/{idProducto}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result = readTask.Result;
                }
            }
            if (result.Correct)
            {
                ViewBag.Mensaje = "Producto eliminado correctamente de la tienda.";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar el producto de la tienda.";
            }
            return PartialView("Modal");
        }
        public ActionResult UpdateStock(int idProducto, int cantidad)
        {
            int idSucursal = int.Parse(Session["IdSucursal"].ToString());
            int idUsuario = int.Parse(Session["idUsuario"].ToString());

            ML.Result result = new ML.Result();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.PutAsJsonAsync($"inventario/{idUsuario}/{idSucursal}/{idProducto}", cantidad);
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result = readTask.Result;
                }
            }
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha actualizado el stock.";
            }
            else
            {
                ViewBag.Mensaje = "Error al actualzizar el stock.";
            }
            return PartialView("Modal");
        }
    }
}