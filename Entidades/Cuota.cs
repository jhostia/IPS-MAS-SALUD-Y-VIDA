using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cuota
    {

        public Cuota()
        {
        }

        public Cuota(long numero, long idPaciente, int tipo, double salario, double valorServicio)
        {
            Numero = numero;
            IdPaciente = idPaciente;
            Tipo = tipo;
            Salario = salario;
            ValorServicio = valorServicio;
        }
        //public void tipoAfiliacion(string tipo)
        //{
        //    Tipo = tipo;
        //}
        public override string ToString()
        {
            return $"{Numero},{IdPaciente},{Tipo},{Salario},{ValorServicio}";
        }
        public long Numero { get; set; }
        public long IdPaciente { get; set; }
        public int Tipo { get; set; }    
        public double Salario { get; set; }
        public double ValorServicio { get; set; }
    }
}
