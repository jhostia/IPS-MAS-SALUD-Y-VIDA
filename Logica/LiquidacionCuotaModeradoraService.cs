using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LiquidacionCuotaModeradoraService
    {
        private LiquidacionCuotaModeradoraRepository repository;
        private decimal SalarioMinimo = 1300606;

        public LiquidacionCuotaModeradoraService(LiquidacionCuotaModeradoraRepository repo)
        {
            repository = repo;
        }

        public LiquidacionCuotaModeradora CalcularCuotaModeradora(LiquidacionCuotaModeradora liquidacion)
        {
            if (liquidacion.TipoAfiliacion.Equals("Contributivo", StringComparison.OrdinalIgnoreCase))
            {
                decimal tarifa = 0;
                decimal topeMaximo = 0;

                if (liquidacion.SalarioDevengado < 2 * SalarioMinimo)
                {
                    tarifa = 0.15m;
                    topeMaximo = 250000;
                }
                else if (liquidacion.SalarioDevengado >= 2 * SalarioMinimo && liquidacion.SalarioDevengado <= 5 * SalarioMinimo)
                {
                    tarifa = 0.20m;
                    topeMaximo = 900000;
                }
                else
                {
                    tarifa = 0.25m;
                    topeMaximo = 1500000;
                }

                decimal cuotaModeradora = liquidacion.ValorServicioHospitalizacion * tarifa;
                liquidacion.CuotaModeradora = Math.Min(cuotaModeradora, topeMaximo);

                // Asignar tarifa y tope máximo
                liquidacion.Tarifa = tarifa;
                liquidacion.TopeMaximo = topeMaximo;
            }
            else if (liquidacion.TipoAfiliacion.Equals("Subsidiado", StringComparison.OrdinalIgnoreCase))
            {
                decimal tarifa = 0.05m;
                decimal topeMaximo = 200000;

                decimal cuotaModeradora = liquidacion.ValorServicioHospitalizacion * tarifa;
                liquidacion.CuotaModeradora = Math.Min(cuotaModeradora, topeMaximo);

                // Asignar tarifa y tope máximo
                liquidacion.Tarifa = tarifa;
                liquidacion.TopeMaximo = topeMaximo;
            }

            return liquidacion;
        }

        // Método para registrar una liquidación
        public void RegistrarLiquidacion(LiquidacionCuotaModeradora liquidacion)
        {
            // Realizar validaciones u operaciones adicionales si es necesario
            // ...

            repository.AgregarLiquidacion(liquidacion);
        }

        // Método para obtener todas las liquidaciones
        public List<LiquidacionCuotaModeradora> ObtenerTodasLasLiquidaciones()
        {
            return repository.ObtenerTodasLasLiquidaciones();
        }

        // Método para obtener el total de liquidaciones por tipo de afiliación
        public Dictionary<string, int> ObtenerTotalLiquidacionesPorTipoAfiliacion()
        {
            var liquidaciones = ObtenerTodasLasLiquidaciones(); // Obtener todas las liquidaciones

            // Usar LINQ para agrupar y contar las liquidaciones por tipo de afiliación
            var totalPorTipoAfiliacion = liquidaciones
                .GroupBy(l => l.TipoAfiliacion, StringComparer.OrdinalIgnoreCase) // Ignorar mayúsculas/minúsculas
                .ToDictionary(g => g.Key, g => g.Count());

            return totalPorTipoAfiliacion;
        }
    }
}
