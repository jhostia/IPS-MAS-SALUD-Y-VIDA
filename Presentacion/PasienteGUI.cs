using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class PasienteGUI
    {
        private ServicioPaciente servicioPaciente = new ServicioPaciente();

        public void Menu()
        {
            int op = 0;
            do
            {
                Console.Clear();
                Console.SetCursorPosition(15, 3); Console.WriteLine("########### MENU PRINCIPAL ############");
                Console.SetCursorPosition(10, 5); Console.WriteLine("1. Agregar pasiente");
                Console.SetCursorPosition(10, 7); Console.WriteLine("2. Mostrar todas los pasientes registradas");
                Console.SetCursorPosition(10, 9); Console.WriteLine("3. Actualizar pasiente");
                Console.SetCursorPosition(10, 11);Console.WriteLine("4. Eliminar pesiente por cedula");
                Console.SetCursorPosition(10, 13); Console.WriteLine("5. Filtar por tipo de afiliaciono");
                Console.SetCursorPosition(10, 15); Console.WriteLine("6. Filtar por nombre");
                Console.SetCursorPosition(10, 17); Console.WriteLine("7. Consultar total cuotas moderadoras liquidadas y valor total liquidado\r\n       " +
                    "      por afiliación régimen subsidiado y régimen contributivo");
                Console.SetCursorPosition(10, 20); Console.WriteLine("8. Salir del programa");
                Console.SetCursorPosition(10, 22); Console.WriteLine("#########Seleccione una opcion#########");

                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        AgregarPasiente();
                        break;
                    case 2:
                        MostrarPasiente();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            } while (op != 8);
        }
        public void AgregarPasiente()
        {
            Console.Clear();
            Console.WriteLine("Ingrese el numero de liquidacion del pasiente");
            int numero = int.Parse(Console.ReadLine());
            Console.WriteLine("Digitela identificacion del pasiente");
            int idPaciente = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite el nombre del paciente");
            string nombre = Console.ReadLine();
            string Nombre = nombre.ToUpper();
            Console.WriteLine("Digite el tipo de afiliacion que tiene la persona");
            Console.WriteLine("Digite 1 si es SUBSIDIADO o Digite 2 si es CONTRIBUTIVO");
            string tipo = Console.ReadLine();
            if (tipo == "1")
            {
                tipo = "Subsidiado";
            }
            else
            {
                tipo = "Contributivo";
            }
            Console.WriteLine("Digite el  salario devengado por el paciente ");
            double salario = double.Parse(Console.ReadLine());
            Console.WriteLine("Digite el  valor del servicio de hospitalización");
            double valorServicio = double.Parse(Console.ReadLine());

            Cuota cuota = new Cuota(numero, idPaciente, nombre, tipo, salario, valorServicio);
            Console.WriteLine(servicioPaciente.Guardar(cuota));
            Console.ReadKey();
        }
        private void MostrarPasiente()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 2); Console.Write("Listado General de los pasientes");
            Console.SetCursorPosition(2, 4);  Console.Write("N_liquidacion");
            Console.SetCursorPosition(18, 4); Console.Write("Cedula");
            Console.SetCursorPosition(29, 4); Console.WriteLine("Nombre");
            Console.SetCursorPosition(38, 4); Console.Write("Afilacion");
            Console.SetCursorPosition(52, 4); Console.Write("Salario");
            Console.SetCursorPosition(62, 4); Console.Write("Valor Atencion");
            Console.SetCursorPosition(78, 4); Console.WriteLine("Cuota");
            Console.SetCursorPosition(88, 4); Console.WriteLine("Tarifa");
            int posicion = 2;

                foreach (var item in servicioPaciente.ConsultarTodos())
                {
                Console.SetCursorPosition(7, 4 + posicion); Console.Write(item.Numero);
                Console.SetCursorPosition(18, 4 + posicion); Console.Write(item.IdPaciente);
                Console.SetCursorPosition(29, 4 + posicion); Console.WriteLine(item.Nombre);
                Console.SetCursorPosition(37, 4 + posicion); Console.Write(item.Tipo);
                Console.SetCursorPosition(52, 4 + posicion); Console.Write(item.Salario);
                Console.SetCursorPosition(65, 4 + posicion); Console.Write(item.ValorServicio);
                Console.SetCursorPosition(78, 4 + posicion); Console.WriteLine(item.VCuota());
                Console.SetCursorPosition(90, 4 + posicion); Console.WriteLine(item.VTarifa());
                posicion++;
            }
            Console.ReadKey();
        }
    }
}
