using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Bitacora
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.BitacoraGetAll().ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Bitacora bitacora = new ML.Bitacora();
                            bitacora.Usuario = new ML.Usuario();

                            bitacora.IdMovimiento = item.IdMovimiento;
                            bitacora.Descripcion = item.Descripcion;
                            bitacora.FechaMovimiento = item.FechaMovimiento.Value;
                            bitacora.Usuario.IdUsuario = item.IdUsuario;
                            bitacora.Usuario.Nombre = item.Nombre;
                            bitacora.Usuario.ApellidoPaterno = item.ApellidoPaterno;
                            bitacora.Usuario.ApellidoMaterno = item.ApellidoMaterno;
                            bitacora.Usuario.Username = item.Username;

                            result.Objects.Add(bitacora);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al consultar la bitacora.";
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
