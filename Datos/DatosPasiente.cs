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
            return $"La informacion de la persona identificada el en numero de cedula {cuota.IdPaciente} fue " +
                $"guardado correctamente";
        }

        public List<Cuota> MostrarTodos()
        {
            List<Cuota> listaCuotas = new List<Cuota>();
            try
            {
                StreamReader lector = new StreamReader(fileName);
                while (!lector.EndOfStream)
                {
                    listaCuotas.Add(Map(lector.ReadLine()));
                }
                lector.Close();
                return listaCuotas;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Cuota Map(string linea)
        {
            var p = new Cuota();
            p.Numero = long.Parse(linea.Split(',')[0]);
            p.IdPaciente = long.Parse(linea.Split(',')[1]);
            p.Tipo = int.Parse(linea.Split(',')[2]);
            p.Salario = double.Parse(linea.Split(',')[3]);
            p.ValorServicio = double.Parse(linea.Split(',')[4]);
            return p;
        }
    }
}
