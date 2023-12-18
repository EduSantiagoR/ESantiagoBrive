using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.SucursalGetAll().ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = item.IdSucursal;
                            sucursal.Nombre = item.Nombre;
                            sucursal.Direccion = item.Direccion;
                            sucursal.Telefono = item.Telefono;
                            result.Objects.Add(sucursal);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se han podido recuperar las sucursales.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int idSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.SucursalGetById(idSucursal).FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Sucursal sucursal = new ML.Sucursal();
                        sucursal.IdSucursal = query.IdSucursal;
                        sucursal.Nombre = query.Nombre;
                        sucursal.Direccion = query.Direccion;
                        sucursal.Telefono = query.Telefono;
                        result.Object = sucursal;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido recuperar la información de la sucursal.";
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
        public static ML.Result Delete(int idUsuario,int idSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.SucursalDelete(idUsuario, idSucursal);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Sucursal eliminada correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar la sucursal.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Add(int idUsuario, ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.SucursalAdd(idUsuario, sucursal.Nombre, sucursal.Direccion, sucursal.Telefono);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Sucursal agregada correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar la sucursal.";
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
        public static ML.Result Update(int idUsuario, ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.SucursalUpdate(idUsuario, sucursal.IdSucursal, sucursal.Nombre, sucursal.Direccion, sucursal.Telefono);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Sucursal actualizada correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al actualizar la sucursal.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
