using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Inventario
    {
        public ML.Sucursal Sucursal { get; set; }
        public ML.Producto Pruducto { get; set; }
        public int Stock { get; set; }
        public List<object> Inventarios { get; set; }
    }
}
