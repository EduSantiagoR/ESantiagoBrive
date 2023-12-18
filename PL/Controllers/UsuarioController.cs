using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            byte[] passwordBytes = Encriptar(Encoding.UTF8.GetBytes(password));
            ML.Result result = BL.Usuario.Login(username, passwordBytes);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Session["IdUsuario"] = usuario.IdUsuario;
                ViewBag.Mensaje = result.Message + $" Bienvenido {usuario.Nombre} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}.";
                ViewBag.Action = "Home";
            }
            else
            {
                ViewBag.Mensaje = result.Message;
                ViewBag.Action = "Login";
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario, string pass)
        {
            usuario.Password = Encriptar(Encoding.UTF8.GetBytes(pass));
            ML.Result result = BL.Usuario.Add(usuario);
            ViewBag.Mensaje = result.Message;
            ViewBag.Action = "Login";
            return PartialView("Modal");
        }
        private static byte[] Encriptar(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] datosEncriptados = sha256.ComputeHash(data);
                return datosEncriptados;
            }
        }
    }
}