using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Username, usuario.Password);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario agregado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al agregar el usuario.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Login(string username, byte[] password)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.UsuarioLogin(username, password).FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Username = query.Username;
                        usuario.Password = query.Password;
                        result.Object = usuario;
                        result.Correct = true;
                        result.Message = "Acceso concedido.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se puede localizar el usuario. Acceso denegado.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
