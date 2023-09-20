using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cuota
    {
        public long numero { get; set; }
        public long idPaciente { get; set; }
        public string tipo { get; set; }    
        public double salario { get; set; }
        public double valorServicio { get; set; }

        public void tipoAfiliacion (string tipo)
        {
            this.tipo = tipo;
        }
    }
}
