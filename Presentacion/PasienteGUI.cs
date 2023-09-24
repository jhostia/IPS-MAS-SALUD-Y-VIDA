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
            Console.WriteLine("Digite 1 si es SUBSIDIADO o Digite 2 si es CONTRIBUTIVO");
            int tipo = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite el  salario devengado por el paciente ");
            double salario = double.Parse(Console.ReadLine());
            Console.WriteLine("Digite el  valor del servicio de hospitalización");
            double valorServicio = double.Parse(Console.ReadLine());

            Cuota cuota = new Cuota(numero, idPaciente, tipo, salario, valorServicio);
            Console.WriteLine(servicioPaciente.Guardar(cuota));
            Console.ReadKey();
        }
        private void MostrarPasiente()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 2); Console.Write("Listado General de los pasientes");
            Console.SetCursorPosition(1, 4); Console.Write("Numero de liquidacion/");
            Console.SetCursorPosition(24, 4); Console.Write("Id del pasiente/");
            Console.SetCursorPosition(41, 4); Console.Write("Tipo de afilacion/");
            Console.SetCursorPosition(60, 4); Console.Write("Salario devengado paciente/");
            Console.SetCursorPosition(88, 4); Console.Write("Valor servicio hospitalizacion");
            int posicion = 2;

                foreach (var item in servicioPaciente.ConsultarTodos())
                {
                Console.SetCursorPosition(10, 4 + posicion); Console.Write(item.Numero);
                Console.SetCursorPosition(28, 4 + posicion); Console.Write(item.IdPaciente);
                Console.SetCursorPosition(49, 4 + posicion); Console.Write(item.Tipo);
                Console.SetCursorPosition(68, 4 + posicion); Console.Write(item.Salario);
                Console.SetCursorPosition(100, 4 + posicion); Console.Write(item.ValorServicio);
                posicion++;
            }
            Console.ReadKey();
        }
    }
}
