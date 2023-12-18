using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Inventario
    {
        public static ML.Result GetAll(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.InventarioGetAll(idProducto).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Inventario registro = new ML.Inventario();
                            registro.Pruducto = new ML.Producto();
                            registro.Sucursal = new ML.Sucursal();

                            registro.Stock = item.Stock;
                            registro.Pruducto.IdProducto = item.IdProducto;
                            registro.Pruducto.Nombre = item.NombreProducto;
                            registro.Pruducto.Precio = item.Precio;
                            registro.Sucursal.IdSucursal = item.IdSucursal;
                            registro.Sucursal.Nombre = item.NombreSucursal;
                            registro.Sucursal.Direccion = item.Direccion;
                            registro.Sucursal.Telefono = item.Telefono;
                            result.Objects.Add(registro);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se han podido recuperar productos.";
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
        public static ML.Result GetProductosAsignados(int idSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.SucursalGetProductosAsignados(idSucursal).ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Inventario registro = new ML.Inventario();
                            registro.Pruducto = new ML.Producto();
                            registro.Pruducto.IdProducto = item.IdProducto;
                            registro.Pruducto.Nombre = item.Nombre;
                            registro.Pruducto.Precio = item.Precio;
                            registro.Stock = item.Stock;
                            result.Objects.Add(registro);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se han podido recuperar los productos asignados.";
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
        public static ML.Result GetProductosNoAsignados(int idSucursal)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.SucursalGetProductosNoAsignados(idSucursal).ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = item.IdProducto;
                            producto.Nombre = item.Nombre;
                            producto.Precio = item.Precio;
                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se han podido recuperar los productos no asignados.";
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
        public static ML.Result Add(int idUsuario, int idSucursal, int idProducto, int stock)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.InventarioAdd(idUsuario, idSucursal, idProducto, stock);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Producto asignado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al asignar producto.";
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
        public static ML.Result Delete(int idUsuario, int idSucursal, int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.InventarioDelete(idUsuario, idSucursal, idProducto);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Eliminado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar producto.";
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
        public static ML.Result Update(int idUsuario, int idSucursal, int idProducto, int stock)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.InventarioUpdate(idUsuario, idSucursal, idProducto, stock);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al actualizar el stock";
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
