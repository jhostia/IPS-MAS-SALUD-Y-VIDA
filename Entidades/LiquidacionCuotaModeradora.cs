using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LiquidacionCuotaModeradora
    {
        public int NumeroLiquidacion { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public string IdentificacionPaciente { get; set; }
        public string TipoAfiliacion { get; set; }
        public decimal SalarioDevengado { get; set; }
        public decimal ValorServicioHospitalizacion { get; set; }
        public decimal CuotaModeradora { get; set; }
        public decimal Tarifa { get; set; }
        public decimal TopeMaximo { get; set; }
    }   
}
