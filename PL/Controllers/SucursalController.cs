using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SucursalController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Sucursal sucursal = new ML.Sucursal();
            sucursal.Sucursales = new List<object>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.GetAsync("sucursal");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach(var itemResult in readTask.Result.Objects)
                    {
                        ML.Sucursal sucursalResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Sucursal>(itemResult.ToString());
                        sucursal.Sucursales.Add(sucursalResult);
                    }
                }
            }
            return View(sucursal);
        }
        [HttpGet]
        public ActionResult Form(int? idSucursal)
        {
            if(idSucursal != null)
            {
                //ML.Result result = BL.Sucursal.GetById(idSucursal.Value);
                ML.Sucursal sucursal = new ML.Sucursal();
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:13046/api/");
                    var taskResponse = client.GetAsync($"sucursal/{idSucursal}");
                    taskResponse.Wait();

                    var resultService = taskResponse.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        sucursal = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Sucursal>(readTask.Result.Object.ToString());
                    }
                }
                return View(sucursal);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Sucursal sucursal)
        {
            int idUsuario = int.Parse(Session["idUsuario"].ToString());
            if(sucursal.IdSucursal == 0)
            {
                ML.Result result = new ML.Result();
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:13046/api/");
                    var taskResponse = client.PostAsJsonAsync($"sucursal/{idUsuario}", sucursal);
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
                    ViewBag.Mensaje = "Sucursal agregada correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error al agregar la sucursal.";
                }
            }
            else
            {
                ML.Result result = new ML.Result();
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:13046/api/");
                    var taskResponse = client.PutAsJsonAsync($"sucursal/{idUsuario}", sucursal);
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
                    ViewBag.Mensaje = "Sucursal actualizada correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error al actualizar la sucursal.";
                }
            }
            return PartialView("Modal");
        }
        public ActionResult Delete(int idSucursal)
        {
            int idUsuario = int.Parse(Session["idUsuario"].ToString());
            ML.Result result = new ML.Result();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13046/api/");
                var taskResponse = client.DeleteAsync($"sucursal/{idSucursal}/{idUsuario}");
                taskResponse.Wait();

                var resultService = taskResponse.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result = readTask.Result;
                }
            }
            ViewBag.Mensaje = result.Message;
            return PartialView("Modal");
        }
    }
}