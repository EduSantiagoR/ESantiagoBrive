using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Productos = new List<object>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.GetAsync("producto");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach(var itemResult in readTask.Result.Objects)
                    {
                        ML.Producto productoResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(itemResult.ToString());
                        producto.Productos.Add(productoResult);
                    }
                }
            }
            return View(producto);
        }
        [HttpGet]
        public ActionResult FormUpdate(int idProducto)
        {
            ML.Inventario inventario = new ML.Inventario();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.GetAsync($"producto/{idProducto}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    inventario.Pruducto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(readTask.Result.Object.ToString());
                }
            }
            return View(inventario);
        }
        [HttpPost]
        public ActionResult FormUpdate(ML.Inventario producto)
        {
            int idUsuario = int.Parse(Session["idUsuario"].ToString());
            ML.Result result = new ML.Result();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.PutAsJsonAsync($"producto/{idUsuario}", producto);
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
                ViewBag.Mensaje = "Producto actualizado correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "Error al actualizar el producto.";
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult FormAdd()
        {
            ML.Inventario inventario = new ML.Inventario();
            inventario.Sucursal = new ML.Sucursal();
            inventario.Sucursal.Sucursales = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.GetAsync("sucursal");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var itemResult in readTask.Result.Objects)
                    {
                        ML.Sucursal sucursalResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Sucursal>(itemResult.ToString());
                        inventario.Sucursal.Sucursales.Add(sucursalResult);
                    }
                }
            }
            return View(inventario);
        }
        [HttpPost]
        public ActionResult FormAdd(ML.Inventario producto)
        {
            int idUsuario = int.Parse(Session["idUsuario"].ToString());
            ML.Result result = new ML.Result();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.PostAsJsonAsync($"producto/{idUsuario}", producto);
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
                ViewBag.Mensaje = "Producto agregado correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "Error al agregar el producto.";
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int idProducto)
        {
            int idUsuario = int.Parse(Session["idUsuario"].ToString());
            ML.Result result = new ML.Result();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.DeleteAsync($"producto/{idUsuario}/{idProducto}");
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
                ViewBag.Mensaje = "Producto eliminado correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar el producto.";
            }
            return PartialView("Modal");
        }
    }
}