using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Bitacora
    {
        public int IdMovimiento { get; set; }
        public ML.Usuario Usuario { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Descripcion { get; set; }
        public List<object> Registros { get; set; }
    }
}
