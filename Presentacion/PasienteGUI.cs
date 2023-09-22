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
                Console.Clear();
                Console.SetCursorPosition(20, 2); Console.WriteLine("########### MENU PRINCIPAL ############");
                Console.SetCursorPosition(10, 5); Console.WriteLine("1. Agregar pasiente");
                Console.SetCursorPosition(10, 7); Console.WriteLine("2. Mostrar todas los pasientes registradas");
                Console.SetCursorPosition(10, 9); Console.WriteLine("3. Actualizar pasiente");
                Console.SetCursorPosition(10, 11); Console.WriteLine("4. Eliminar pesiente por cedula");
                Console.SetCursorPosition(10, 13); Console.WriteLine("5. Salir del programa");
                Console.SetCursorPosition(20, 13); Console.WriteLine("#########Seleccione una opcion#########");
            } while (op != 5);
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
        }
        public void AgregarPasiente()
        {
            Console.Clear();
            Console.WriteLine("Ingrese el numero de liquidacion del pasiente");
            long numero = long.Parse(Console.ReadLine());
            Console.WriteLine("Digitela identificacion del pasiente");
            long idPaciente = long.Parse(Console.ReadLine());
            Console.WriteLine("Digite el tipo de adiliacion que tiene la persona");
            string tipo = Console.ReadLine();
            Console.WriteLine("Digite el salario del pasiente");
            double salario =double.Parse(Console.ReadLine());
            Console.WriteLine("Digite el sexo de la persona");
            double valorServicio = double.Parse(Console.ReadLine());

            Persona persona = new Persona(cedula, nombre, apellido, edad, sexo, pulsaciones);
            Console.WriteLine(personaServicio.Guardar(persona));
            Console.ReadKey();
        }

    }
}
