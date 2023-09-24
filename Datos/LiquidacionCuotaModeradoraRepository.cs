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

                        //Parsear la fecha y hora en varios formatos
                        DateTime fechaLiquidacion;
                        if (DateTime.TryParseExact(campos[1], new string[] { "dd/MM/yyyy HH:mm:ss tt", "dd/MM/yyyy h:mm:ss tt", "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy h:mm:ss" },
                                                   CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaLiquidacion))
                        {
                            liquidacion.FechaLiquidacion = fechaLiquidacion;
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

        // Método para actualizar una liquidación
        public bool ActualizarLiquidacion(LiquidacionCuotaModeradora liquidacion)
        {
            bool liquidacionEncontrada = false;
            string[] lineas = File.ReadAllLines(archivoLiquidaciones);

            for (int i = 0; i < lineas.Length; i++)
            {
                string linea = lineas[i];
                string[] campos = linea.Split(';');

                if (campos.Length == 9 && int.Parse(campos[0]) == liquidacion.NumeroLiquidacion)
                {
                    // Si se encuentra la liquidación, se actualiza la línea
                    lineas[i] = $"{liquidacion.NumeroLiquidacion};{liquidacion.FechaLiquidacion};{liquidacion.IdentificacionPaciente};{liquidacion.TipoAfiliacion};{liquidacion.SalarioDevengado};{liquidacion.ValorServicioHospitalizacion};{liquidacion.CuotaModeradora};{liquidacion.Tarifa};{liquidacion.TopeMaximo}";
                    liquidacionEncontrada = true;
                    break; // Terminamos el bucle, ya que encontramos y actualizamos la liquidación
                }
            }

            if (liquidacionEncontrada)
            {
                // Sobreescribe el archivo con la línea actualizada
                File.WriteAllLines(archivoLiquidaciones, lineas);
                return true;
            }
            else
            {
                Console.WriteLine("La liquidación con el número especificado no existe.");
                return false; // No se encontró la liquidación
            }
        }

        //Metodo para obtener la liquidacion por su numero correspodiente
        public LiquidacionCuotaModeradora ObtenerLiquidacionPorNumero(int numeroLiquidacion)
        {
            if (File.Exists(archivoLiquidaciones))
            {
                string[] lineas = File.ReadAllLines(archivoLiquidaciones);
                foreach (string linea in lineas)
                {
                    string[] campos = linea.Split(';');

                    if (campos.Length == 9)
                    {
                        int numero = int.Parse(campos[0]);

                        if (numero == numeroLiquidacion)
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

                            DateTime fechaLiquidacion;
                            if (DateTime.TryParseExact(campos[1], new string[] { "dd/MM/yyyy HH:mm:ss tt", "dd/MM/yyyy h:mm:ss tt", "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy h:mm:ss" },
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaLiquidacion))
                            {
                                liquidacion.FechaLiquidacion = fechaLiquidacion;
                            }

                            return liquidacion;
                        }
                    }
                }
            }

            return null; // No se encontró una liquidación con ese número
        }

        //Metodo para obtener las liquidaciones por su año y mes 
        public List<LiquidacionCuotaModeradora> ObtenerLiquidacionesPorMesYAnio(int mes, int anio)
        {
            List<LiquidacionCuotaModeradora> liquidacionesFiltradas = new List<LiquidacionCuotaModeradora>();

            if (File.Exists(archivoLiquidaciones))
            {
                string[] lineas = File.ReadAllLines(archivoLiquidaciones);
                foreach (string linea in lineas)
                {
                    string[] campos = linea.Split(';');

                    if (campos.Length == 9)
                    {
                        DateTime fechaLiquidacion;
                        if (DateTime.TryParseExact(campos[1], "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaLiquidacion))
                        {
                            if (fechaLiquidacion.Year == anio && fechaLiquidacion.Month == mes)
                            {
                                LiquidacionCuotaModeradora liquidacion = new LiquidacionCuotaModeradora
                                {
                                    NumeroLiquidacion = int.Parse(campos[0]),
                                    FechaLiquidacion = fechaLiquidacion,
                                    IdentificacionPaciente = campos[2],
                                    TipoAfiliacion = campos[3],
                                    SalarioDevengado = decimal.Parse(campos[4]),
                                    ValorServicioHospitalizacion = decimal.Parse(campos[5]),
                                    CuotaModeradora = decimal.Parse(campos[6]),
                                    Tarifa = decimal.Parse(campos[7]),
                                    TopeMaximo = decimal.Parse(campos[8])
                                };

                                liquidacionesFiltradas.Add(liquidacion);
                            }
                        }
                    }
                }
            }
            return liquidacionesFiltradas;
        }
    }
}
