using Datos;
using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repo = new LiquidacionCuotaModeradoraRepository(); // Instancia del repositorio
            var service = new LiquidacionCuotaModeradoraService(repo); // Instancia del servicio

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Menú Principal ===");
                Console.WriteLine("1. Registrar Liquidación");
                Console.WriteLine("2. Mostrar Todas las Liquidaciones");
                Console.WriteLine("3. Cantidad de liquidaciones por tipo de afiliacion");
                Console.WriteLine("4. Valor total de cuotas moderadas y total liquidado por tipo de afiliacion");
                Console.WriteLine("5. Mostrar liquidaciones por mes y año");
                Console.WriteLine("6. Eliminar una liquidacion");
                Console.WriteLine("7. Modificar una liquidacion");
                Console.WriteLine("8. Salir");
                Console.WriteLine("");
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out int opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("=== Registro de Liquidación ===");
                            RegistrarLiquidacion(service);
                            break;
                        case 2:
                            MostrarTodasLasLiquidaciones(service);
                            break;
                        case 3:
                            MostrarTotalLiquidacionesPorTipoAfiliacion(service);
                            break;
                        case 4:
                            MostrarTotalesPorTipoAfiliacion(service);
                            break;
                        case 5:
                            MostrarLiquidacionesPorMesYAnio(service);
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("=== Eliminar Liquidación por Número ===");
                            EliminarLiquidacion(service);
                            break;
                        case 7:
                            Console.Clear();
                            Console.WriteLine("=== Modificar Valor del Servicio y Recalcular Cuota Moderadora ===");
                            ModificarValorServicioYRecalcularCuota(service);
                            break;
                        case 8:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Intente de nuevo.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                }

                // Pausa para que el usuario pueda ver los resultados antes de regresar al menú principal
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
        static void RegistrarLiquidacion(LiquidacionCuotaModeradoraService service)
        {
            Console.Clear();
            Console.WriteLine("=== Registro de Liquidación ===");

            // Solicitar datos de la liquidación al usuario
            Console.Write("Número de Liquidación: ");
            int numeroLiquidacion = int.Parse(Console.ReadLine());

            // Obtener automáticamente la fecha y hora actual
            DateTime fechaLiquidacion = DateTime.Now;

            Console.Write("Identificación del Paciente: ");
            string identificacionPaciente = Console.ReadLine();

            Console.Write("Tipo de Afiliación (Contributivo o Subsidiado): ");
            string tipoAfiliacion = Console.ReadLine();

            Console.Write("Salario Devengado del Paciente: ");
            decimal salarioDevengado = decimal.Parse(Console.ReadLine());

            Console.Write("Valor del Servicio de Hospitalización Prestado: ");
            decimal valorServicioHospitalizacion = decimal.Parse(Console.ReadLine());

            // Crear una instancia de LiquidacionCuotaModeradora con los datos ingresados
            var liquidacion = new LiquidacionCuotaModeradora
            {
                NumeroLiquidacion = numeroLiquidacion,
                FechaLiquidacion = fechaLiquidacion, // Asignar la fecha y hora actual
                IdentificacionPaciente = identificacionPaciente,
                TipoAfiliacion = tipoAfiliacion,
                SalarioDevengado = salarioDevengado,
                ValorServicioHospitalizacion = valorServicioHospitalizacion
            };

            // Calcular la cuota moderadora y asignar la tarifa y el tope máximo
            liquidacion = service.CalcularCuotaModeradora(liquidacion);

            // Registrar la liquidación en el repositorio
            service.RegistrarLiquidacion(liquidacion);

            Console.WriteLine("Liquidación registrada exitosamente.");
            Console.Clear();
        }
        static void MostrarTodasLasLiquidaciones(LiquidacionCuotaModeradoraService service)
        {
            Console.Clear();
            Console.WriteLine("=== Todas las Liquidaciones ===");
            var liquidaciones = service.ObtenerTodasLasLiquidaciones();

            if (liquidaciones.Count == 0)
            {
                Console.WriteLine("No hay liquidaciones registradas.");
            }
            else
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("| Número de Liquidación | Fecha de Liquidación      | Identificación del Paciente | Tipo de Afiliación | Salario Devengado | Valor del Servicio de Hospitalización | Tarifa Aplicada | Valor Liquidado | Aplicó Tope Máximo | Valor de la Cuota Moderadora |");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                foreach (var liquidacion in liquidaciones)
                {
                    string aplicoTopeMaximo = liquidacion.CuotaModeradora == liquidacion.TopeMaximo ? "Sí" : "No";

                    Console.WriteLine($"| {liquidacion.NumeroLiquidacion,-21} | {liquidacion.FechaLiquidacion.ToString("dd/MM/yyyy HH:mm:ss tt"),-24} | {liquidacion.IdentificacionPaciente,-30} | {liquidacion.TipoAfiliacion,-17} | {liquidacion.SalarioDevengado,-16:C} | {liquidacion.ValorServicioHospitalizacion,-44:C} | {liquidacion.Tarifa * 100,-15:N2}% | {liquidacion.CuotaModeradora,-15:C} | {aplicoTopeMaximo,-16} | {liquidacion.TopeMaximo,-27:C} |");
                }

                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }
        static void MostrarTotalLiquidacionesPorTipoAfiliacion(LiquidacionCuotaModeradoraService service)
        {
            Console.Clear();
            Console.WriteLine("=== Total de Liquidaciones por Tipo de Afiliación ===");
            var totalPorTipoAfiliacion = service.ObtenerTotalLiquidacionesPorTipoAfiliacion();

            foreach (var kvp in totalPorTipoAfiliacion)
            {
                Console.WriteLine($"Tipo de Afiliación: {kvp.Key}, Total de Liquidaciones: {kvp.Value}");
            }


        }

        static void MostrarTotalesPorTipoAfiliacion(LiquidacionCuotaModeradoraService service)
        {
            Console.Clear();
            Console.WriteLine("=== Totales por Tipo de Afiliación ===");

            var totalCuotasPorTipo = service.ObtenerTotalCuotasModeradorasPorTipoAfiliacion();
            var totalLiquidadoPorTipo = service.ObtenerTotalLiquidadoPorTipoAfiliacion();

            foreach (var tipoAfiliacion in totalCuotasPorTipo.Keys)
            {
                decimal totalCuotas = totalCuotasPorTipo[tipoAfiliacion];
                decimal totalLiquidado = totalLiquidadoPorTipo[tipoAfiliacion];

                Console.WriteLine($"Tipo de Afiliación: {tipoAfiliacion}");
                Console.WriteLine($"Total de Cuotas Moderadoras: {totalCuotas:C}");
                Console.WriteLine($"Total Liquidado: {totalLiquidado:C}");
                Console.WriteLine();
            }
        }
        static void EliminarLiquidacion(LiquidacionCuotaModeradoraService service)
        {
            Console.Clear();
            Console.WriteLine("=== Eliminar Liquidación por Número ===");
            Console.Write("Ingrese el número de liquidación que desea eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int numeroLiquidacion))
            {
                bool eliminada = service.EliminarLiquidacionPorNumero(numeroLiquidacion);
                if (eliminada)
                {
                    Console.WriteLine($"La liquidación con número {numeroLiquidacion} ha sido eliminada exitosamente.");
                }
                else
                {
                    Console.WriteLine($"No se encontró ninguna liquidación con el número {numeroLiquidacion}.");
                }
            }
            else
            {
                Console.WriteLine("Número de liquidación inválido.");
            }

        }

        static void ModificarValorServicioYRecalcularCuota(LiquidacionCuotaModeradoraService service)
        {
            Console.Clear();
            Console.WriteLine("=== Modificar Valor del Servicio y Recalcular Cuota Moderadora ===");
            Console.Write("Ingrese el número de liquidación que desea modificar: ");
            if (int.TryParse(Console.ReadLine(), out int numeroLiquidacion))
            {
                // Verificar si la liquidación existe antes de continuar
                var liquidacionExistente = service.ObtenerLiquidacionPorNumero(numeroLiquidacion);
                if (liquidacionExistente != null)
                {
                    Console.Write("Ingrese el nuevo valor del servicio de hospitalización: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal nuevoValorServicio))
                    {
                        bool modificado = service.ModificarValorServicioYRecalcularCuotaModeradora(numeroLiquidacion, nuevoValorServicio);
                        if (modificado)
                        {
                            Console.WriteLine($"La liquidación con número {numeroLiquidacion} ha sido modificada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine($"No se encontró ninguna liquidación con el número {numeroLiquidacion}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Valor del servicio inválido.");
                    }
                }
                else
                {
                    Console.WriteLine($"No se encontró ninguna liquidación con el número {numeroLiquidacion}.");
                }
            }
            else
            {
                Console.WriteLine("Número de liquidación inválido.");
            }
        }
        static void MostrarLiquidacionesPorMesYAnio(LiquidacionCuotaModeradoraService service)
        {
            Console.Clear();
            Console.WriteLine("=== Consultar Liquidaciones por Mes y Año ===");
            Console.Write("Ingrese el año (yyyy): ");
            if (int.TryParse(Console.ReadLine(), out int anio))
            {
                Console.Write("Ingrese el mes (1-12): ");
                if (int.TryParse(Console.ReadLine(), out int mes) && mes >= 1 && mes <= 12)
                {
                    var liquidacionesFiltradas = service.ObtenerLiquidacionesPorMesYAnio(mes, anio);

                    if (liquidacionesFiltradas.Count == 0)
                    {
                        Console.WriteLine($"No hay liquidaciones registradas para el mes {mes} del año {anio}.");
                    }
                    else
                    {
                        Console.WriteLine($"=== Liquidaciones para el mes {mes} del año {anio} ===");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("| Número de Liquidación | Identificación del Paciente | Tipo de Afiliación | Salario Devengado | Valor del Servicio de Hospitalización | Tarifa Aplicada | Valor Liquidado | Aplicó Tope Máximo | Valor de la Cuota Moderadora |");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

                        foreach (var liquidacion in liquidacionesFiltradas)
                        {
                            string aplicoTopeMaximo = liquidacion.CuotaModeradora == liquidacion.TopeMaximo ? "Sí" : "No";

                            Console.WriteLine($"| {liquidacion.NumeroLiquidacion,-21} | {liquidacion.IdentificacionPaciente,-30} | {liquidacion.TipoAfiliacion,-17} | {liquidacion.SalarioDevengado,-16:C} | {liquidacion.ValorServicioHospitalizacion,-44:C} | {liquidacion.Tarifa * 100,-15:N2}% | {liquidacion.CuotaModeradora,-15:C} | {aplicoTopeMaximo,-16} | {liquidacion.TopeMaximo,-27:C} |");
                        }

                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("Mes inválido. Debe ser un valor entre 1 y 12.");
                }
            }
            else
            {
                Console.WriteLine("Año inválido. Ingrese un año en formato yyyy.");
            }


        }
    }
}
