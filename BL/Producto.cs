using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.ProductoGetAll().ToList();
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
        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    var query = context.ProductoGetById(idProducto).FirstOrDefault();
                    if( query != null)
                    {
                        result.Object = new object();
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.Precio = query.Precio;
                        result.Object = producto;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al consultar el producto.";
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
        public static ML.Result Add(int idUsuario, ML.Inventario producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.ProductoAdd(idUsuario, producto.Pruducto.Nombre, producto.Pruducto.Precio, producto.Sucursal.IdSucursal, producto.Stock);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Producto agregado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al agregar el producto.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = true;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(int idUsuario, ML.Inventario producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.ProductoUpdate(idUsuario, producto.Pruducto.IdProducto, producto.Pruducto.Nombre, producto.Pruducto.Precio);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Producto actualizado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al actualizar el producto.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = true;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int idUsuario, int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoBriveEntities context = new DL.ESantiagoBriveEntities())
                {
                    int rowsAffected = context.ProductoDelete(idUsuario, idProducto);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Producto eliminado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar el producto.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = true;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
