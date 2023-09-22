using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ServicioPasiente
    {
        //private readonly DatosPasiente datosPasiente;
        DatosPasiente datosPasiente = null;
        private List<Cuota> cuotaList = null;
        public ServicioPasiente()
        {
            datosPasiente = new DatosPasiente();
            cuotaList = datosPasiente.ConsultarTodos();
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
        //public ConsultaPersonaResponse ConsultarTodos()
        //{

        //    try
        //    {
        //        List<Cuota> cuota = datosPasiente.ConsultarTodos();
        //        if (cuota != null)
        //        {
        //            return new ConsultaPersonaResponse(cuota);
        //        }
        //        else
        //        {
        //            return new ConsultaPersonaResponse("La Persona buscada no se encuentra Registrada");
        //        }

        //    }
        //    catch (Exception e)
        //    {

        //        return new ConsultaPersonaResponse("Error de Aplicacion: " + e.Message);
        //    }
        //}
        public class ConsultaPersonaResponse
        {
            public List<Cuota> Cuotas { get; set; }
            public string Message { get; set; }
            public bool Encontrado { get; set; }

            public ConsultaPersonaResponse(List<Cuota> cuotas)
            {
                Cuotas = new List<Cuota>();
                Cuotas = cuotas;
                Encontrado = true;
            }
            public ConsultaPersonaResponse(string message)
            {
                Message = message;
                Encontrado = false;
            }
        }
        //public List<Cuota> ConsultarTodos()
        //{
        //    return cuotaList;//retorna la lista 
        //}
    }
}
