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
        public string Guardar(LiquidacionCuotaModeradora cuota)
        {
            var escritor = new StreamWriter(fileName, true);
            escritor.WriteLine(cuota.ToString()); 
            escritor.Close();
            return $"La informacion de la persona identificada el en numero de cedula {cuota.IdPaciente} fue " +
                $"guardado correctamente";
        }

        public List<LiquidacionCuotaModeradora> MostrarTodos()
        {
            List<LiquidacionCuotaModeradora> listaCuotas = new List<LiquidacionCuotaModeradora>();
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

        public LiquidacionCuotaModeradora Map(string linea)
        {
            var p = new LiquidacionCuotaModeradora();
            p.Numero = int.Parse(linea.Split(',')[0]);
            p.IdPaciente = int.Parse(linea.Split(',')[1]);
            p.Nombre = linea.Split(',')[2];
            p.Tipo = linea.Split(',')[3];
            p.Salario = double.Parse(linea.Split(',')[4]);
            p.ValorServicio = double.Parse(linea.Split(',')[5]);
            return p;
        }
    }
}
