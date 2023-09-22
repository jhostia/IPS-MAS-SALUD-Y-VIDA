using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosPasiente
    {
        string fileName = "PasienteInfo.txt";
        public string Guardar(Cuota cuota)
        {
            var escritor = new StreamWriter(fileName, true);
            escritor.WriteLine(cuota.ToString()); 
            escritor.Close();
            return $"La informacion de la persona identificada el en numero de cedula {cuota.idPaciente} fue " +
                $"guardado correctamente";
        }
    }
}
