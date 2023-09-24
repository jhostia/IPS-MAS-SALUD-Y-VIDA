using Datos;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repo = new LiquidacionCuotaModeradoraRepository(); // Instancia del repositorio
            var service = new LiquidacionCuotaModeradoraService(repo); // Instancia del servicio

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Menú Principal ===");
                Console.WriteLine("1. Registrar Liquidación");
                Console.WriteLine("2. Mostrar Todas las Liquidaciones");
                Console.WriteLine("3. Cantidad de liquidaciones por tipo de afiliacion");
                Console.WriteLine("4. Valor total de cuotas moderadas y total liquidado por tipo de afiliacion");
                Console.WriteLine("5. Mostrar liquidaciones por mes y año");
                Console.WriteLine("6. Eliminar una liquidacion");
                Console.WriteLine("7. Modificar una liquidacion");
                Console.WriteLine("8. Salir");
                Console.WriteLine("");
                Console.Write("Seleccione una opción: ");
            }
    }
}
