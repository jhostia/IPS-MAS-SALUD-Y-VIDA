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
        public Cuota(int numero, int idPaciente,string nombre, string tipo, double salario, double valorServicio)
        {
            Numero = numero;
            IdPaciente = idPaciente;
            Nombre = nombre;
            Tipo = tipo;
            Salario = salario;
            ValorServicio = valorServicio;
        }
        public int VTarifa()
        {
            int tarifa = 0;
            double salarioMinimo = 1000000;

            if (Tipo == "Contributivo")
            {
                if (Salario < 2 * salarioMinimo)
                {
                    tarifa = 15;
                }
                else if ((Salario >= 2 * salarioMinimo) && (Salario < 5 * salarioMinimo))
                {
                    tarifa = 20;
                }
                else if (Salario >= 5 * salarioMinimo)
                {
                    tarifa = 25;
                }
            }
            else if (Tipo == "Subsidiado")
            {
                tarifa = 5;
            }
            return tarifa;
        }
        public double VCuota()
        {
            double tarifa = VTarifa();
            double cuotaModeradora;

            cuotaModeradora = ValorServicio * tarifa;

            if (Tipo == "Contributivo")
            {
                if (cuotaModeradora > 250000)
                {
                    if (tarifa == 15 && cuotaModeradora > 250000)
                    {
                        cuotaModeradora = 250000;
                    }
                    else if (tarifa == 20 && cuotaModeradora > 900000)
                    {
                        cuotaModeradora = 900000;
                    }
                    else if (tarifa == 15 && cuotaModeradora > 1500000)
                    {
                        cuotaModeradora = 1500000;
                    }
                }
            }
            else
            {
                if (tarifa == 5 && cuotaModeradora > 200000)
                {
                    cuotaModeradora = 200000;
                }
            }

            return cuotaModeradora;
        }
        //public void tipoAfiliacion(string tipo)
        //{
        //    Tipo = tipo;
        //}
        public override string ToString()
        {
            return $"{Numero},{IdPaciente},{Nombre},{Tipo},{Salario},{ValorServicio}";
        }
        public int Numero { get; set; }
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public double Salario { get; set; }
        public double ValorServicio { get; set; }
        public DateTime FechaLiquidacion { get; set; }

    }
}
