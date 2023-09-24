using Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class LiquidacionCuotaModeradoraRepository
    {
        private string archivoLiquidaciones = "liquidaciones.txt";

        // Método para agregar una liquidación
        public void AgregarLiquidacion(LiquidacionCuotaModeradora liquidacion)
        {
            using (StreamWriter writer = File.AppendText(archivoLiquidaciones))
            {
                string linea = $"{liquidacion.NumeroLiquidacion};{liquidacion.FechaLiquidacion};{liquidacion.IdentificacionPaciente};{liquidacion.TipoAfiliacion};{liquidacion.SalarioDevengado};{liquidacion.ValorServicioHospitalizacion};{liquidacion.CuotaModeradora};{liquidacion.Tarifa};{liquidacion.TopeMaximo}";


                writer.WriteLine(linea);
            }
        }

        // Método para obtener todas las liquidaciones
        public List<LiquidacionCuotaModeradora> ObtenerTodasLasLiquidaciones()
        {
            List<LiquidacionCuotaModeradora> liquidaciones = new List<LiquidacionCuotaModeradora>();
            if (File.Exists(archivoLiquidaciones))
            {
                string[] lineas = File.ReadAllLines(archivoLiquidaciones);
                foreach (string linea in lineas)
                {
                    string[] campos = linea.Split(';');

                    if (campos.Length == 9)
                    {
                        LiquidacionCuotaModeradora liquidacion = new LiquidacionCuotaModeradora
                        {
                            NumeroLiquidacion = int.Parse(campos[0]),
                            IdentificacionPaciente = campos[2],
                            TipoAfiliacion = campos[3],
                            SalarioDevengado = decimal.Parse(campos[4]),
                            ValorServicioHospitalizacion = decimal.Parse(campos[5]),
                            CuotaModeradora = decimal.Parse(campos[6]),
                            Tarifa = decimal.Parse(campos[7]),
                            TopeMaximo = decimal.Parse(campos[8])
                        };

                        // Intentar parsear la fecha y hora en varios formatos
                        DateTime fechaLiquidacion;
                        if (DateTime.TryParseExact(campos[1], new string[] { "dd/MM/yyyy HH:mm:ss tt", "dd/MM/yyyy h:mm:ss tt", "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy h:mm:ss" },
                                                   CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaLiquidacion))
                        {
                            liquidacion.FechaLiquidacion = fechaLiquidacion;
                        }
                        else
                        {
                            // Manejar el caso en el que la fecha no pueda ser parseada
                            // Puedes agregar un registro de error o realizar alguna acción adecuada.
                        }

                        liquidaciones.Add(liquidacion);
                    }
                }
            }
            return liquidaciones;
        }

        //Metodo para eliminar una liquidacion por el numero
        public bool EliminarLiquidacion(int numeroLiquidacion)
        {
            List<string> lineas = new List<string>();

            if (File.Exists(archivoLiquidaciones))
            {
                string[] todasLasLineas = File.ReadAllLines(archivoLiquidaciones);

                bool liquidacionEncontrada = false;

                foreach (string linea in todasLasLineas)
                {
                    string[] campos = linea.Split(';');

                    if (campos.Length == 9 && int.Parse(campos[0]) != numeroLiquidacion)
                    {
                        // Conservar las líneas de liquidaciones que no coincidan con el número de liquidación a eliminar
                        lineas.Add(linea);
                    }
                    else if (campos.Length == 9 && int.Parse(campos[0]) == numeroLiquidacion)
                    {
                        // Se encontró la liquidación a eliminar
                        liquidacionEncontrada = true;
                    }
                }

                if (liquidacionEncontrada)
                {
                    // Sobreescribir el archivo con las líneas actualizadas
                    File.WriteAllLines(archivoLiquidaciones, lineas);
                    return true; // La liquidación fue eliminada con éxito
                }
                else
                {
                    return false; // No se encontró la liquidación a eliminar
                }
            }
            else
            {
                return false; // El archivo de liquidaciones no existe
            }
        }
    }
}
