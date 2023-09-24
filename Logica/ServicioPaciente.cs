using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ServicioPaciente
    {
        LiquidacionCuotaModeradoraRepository datosPasiente = null;
        private List<LiquidacionCuotaModeradora> listaP = null;
        public ServicioPaciente()
        {
            datosPasiente = new LiquidacionCuotaModeradoraRepository();
            listaP = datosPasiente.MostrarTodos();
        }
        public String Guardar(LiquidacionCuotaModeradora cuota)
        { 
            if (cuota == null)
            {
                return "No se puede agregar personas nulas o sin in formacion";
            }
            var msg = (datosPasiente.Guardar(cuota));
            return msg;
        }
        public List<LiquidacionCuotaModeradora> ConsultarTodos()
        {
            return listaP;
        }
    }
}
