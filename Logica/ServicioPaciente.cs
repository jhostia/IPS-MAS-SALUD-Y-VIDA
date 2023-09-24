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
        DatosPasiente datosPasiente = null;
        private List<Cuota> listaP = null;
        public ServicioPaciente()
        {
            datosPasiente = new DatosPasiente();
            listaP = datosPasiente.MostrarTodos();
        }
        public String Guardar(Cuota cuota)
        { 
            if (cuota == null)
            {
                return "No se puede agregar personas nulas o sin in formacion";
            }
            var msg = (datosPasiente.Guardar(cuota));
            return msg;
        }
        public List<Cuota> ConsultarTodos()
        {
            return listaP;
        }
    }
}
