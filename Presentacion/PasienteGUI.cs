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
        private ServicioPasiente servicioPasiente = new ServicioPasiente();

        public void Menu()
        {
            int op = 0;
            do
            {
                Console.Clear();
                Console.SetCursorPosition(15, 2); Console.WriteLine("########### MENU PRINCIPAL ############");
                Console.SetCursorPosition(10, 5); Console.WriteLine("1. Agregar pasiente");
                Console.SetCursorPosition(10, 7); Console.WriteLine("2. Mostrar todas los pasientes registradas");
                Console.SetCursorPosition(10, 9); Console.WriteLine("3. Actualizar pasiente");
                Console.SetCursorPosition(10, 11); Console.WriteLine("4. Eliminar pesiente por cedula");
                Console.SetCursorPosition(10, 13); Console.WriteLine("5. Salir del programa");
                Console.SetCursorPosition(15, 15); Console.WriteLine("#########Seleccione una opcion#########");

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
                    case 5:
                        break;
                }
            } while (op != 5);
        }
        public void AgregarPasiente()
        {
            Console.Clear();
            Console.WriteLine("Ingrese el numero de liquidacion del pasiente");
            long numero = long.Parse(Console.ReadLine());
            Console.WriteLine("Digitela identificacion del pasiente");
            long idPaciente = long.Parse(Console.ReadLine());
            Console.WriteLine("Digite el tipo de afiliacion que tiene la persona");
            string tipo = Console.ReadLine();
            Console.WriteLine("Digite el salario del pasiente");
            double salario = double.Parse(Console.ReadLine());
            Console.WriteLine("Digite el salario de la persona");
            double valorServicio = double.Parse(Console.ReadLine());

            Cuota cuota = new Cuota(numero, idPaciente, tipo, salario, valorServicio);
            Console.WriteLine(servicioPasiente.Guardar(cuota));
            Console.ReadKey();
        }
        private void MostrarPasiente()
        {
            Console.Clear();
            Console.SetCursorPosition(15, 2); Console.Write("Listado General de los pasientes");
            Console.SetCursorPosition(10, 4); Console.Write("Numero de liquidacion:");
            Console.SetCursorPosition(28, 4); Console.Write("Identificacion del paciente:");
            Console.SetCursorPosition(34, 4); Console.Write("Tipo de afilacion");
            Console.SetCursorPosition(40, 4); Console.Write("Salario devengado por el paciente:");
            Console.SetCursorPosition(46, 4); Console.Write("Valor del servicio de hospitalizacion:");
            int posicion = 2;
            foreach (var item in servicioPasiente.ConsultarTodos())
            {

                Console.SetCursorPosition(15, 4 + posicion); Console.Write(item.Numero);
                Console.SetCursorPosition(29, 4 + posicion); Console.Write(item.IdPaciente);
                Console.SetCursorPosition(34, 4 + posicion); Console.Write(item.Tipo);
                Console.SetCursorPosition(42, 4 + posicion); Console.Write(item.Salario);
                Console.SetCursorPosition(48, 4 + posicion); Console.Write(item.ValorServicio);
                posicion++;
            }
            Console.ReadKey();
        }
    }
}
