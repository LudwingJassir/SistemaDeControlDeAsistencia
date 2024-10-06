using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // Para usar el método Any()

namespace SistemaAsistencia
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lista para almacenar los estudiantes
            List<Universidad.Estudiante> estudiantes = new List<Universidad.Estudiante>();

            bool agregarOtro = true;

            // Ciclo para ingresar estudiantes hasta que el usuario decida detenerse
            while (agregarOtro)
            {
                // Crear un nuevo estudiante
                var estudiante = new Universidad.Estudiante();

                // Validar entrada de nombre (solo letras y espacios)
                do
                {
                    Console.WriteLine("Ingrese el nombre del estudiante:");
                    estudiante.Nombre = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(estudiante.Nombre) || estudiante.Nombre.Any(char.IsDigit))
                    {
                        Console.WriteLine("Por favor, ingrese un nombre valido sin números o caracteres especiales.");
                    }
                } while (string.IsNullOrWhiteSpace(estudiante.Nombre) || estudiante.Nombre.Any(char.IsDigit));

                // Validar entrada de número de sesiones totales
                do
                {
                    Console.WriteLine("Ingrese el numero de sesiones totales:");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int sesionesTotales) || sesionesTotales <= 0)
                    {
                        Console.WriteLine("Por favor, ingrese un numero válido de sesiones totales.");
                    }
                    else
                    {
                        estudiante.SesionesTotales = sesionesTotales;
                    }
                } while (estudiante.SesionesTotales <= 0);

                // Validar entrada de número de sesiones asistidas
                do
                {
                    Console.WriteLine("Ingrese el numero de sesiones asistidas:");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int sesionesAsistidas) || sesionesAsistidas < 0 || sesionesAsistidas > estudiante.SesionesTotales)
                    {
                        Console.WriteLine("Por favor, ingrese un numero válido de sesiones asistidas (entre 0 y el total de sesiones).");
                    }
                    else
                    {
                        estudiante.SesionesAsistidas = sesionesAsistidas;
                    }
                } while (estudiante.SesionesAsistidas < 0 || estudiante.SesionesAsistidas > estudiante.SesionesTotales);

                // Añadir el estudiante a la lista
                estudiantes.Add(estudiante);

                // Preguntar si desea agregar otro estudiante
                Console.WriteLine("¿Desea agregar otro estudiante? (S/N)");
                string respuesta = Console.ReadLine();

                if (respuesta.ToLower() == "n")
                {
                    agregarOtro = false;
                }
            }

            // Mostrar la información de los estudiantes.
            foreach (var estudiante in estudiantes)
            {
                estudiante.MostrarInformacion();
            }

            // Preguntar al usuario si desea exportar la información a un archivo .txt
            Console.WriteLine("\n¿Desea exportar los datos de los estudiantes a un archivo? (S/N)");
            string exportarRespuesta = Console.ReadLine();

            if (exportarRespuesta.ToLower() == "s")
            {
                foreach (var estudiante in estudiantes)
                {
                    ExportarDatos(estudiante);
                }
                Console.WriteLine("Datos exportados correctamente a la carpeta Descargas.");
            }
        }

        // Método para exportar la información del estudiante a un archivo .txt
        // El archivo .txt se exportará a la carpeta Descargas de Windows.
        static void ExportarDatos(Universidad.Estudiante estudiante)
        {
            // Obtener la carpeta Descargas de Windows
            string carpetaDescargas = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            string path = Path.Combine(carpetaDescargas, $"{estudiante.Nombre}_Asistencia.txt");

            double porcentajeAsistencia = Universidad.Estudiante.Asistencia.CalcularPorcentaje(estudiante.SesionesTotales, estudiante.SesionesAsistidas);
            bool cumple = Universidad.Estudiante.Asistencia.CumpleMinimo(porcentajeAsistencia);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine($"Nombre: {estudiante.Nombre}");
                sw.WriteLine($"Sesiones Totales: {estudiante.SesionesTotales}");
                sw.WriteLine($"Sesiones Asistidas: {estudiante.SesionesAsistidas}");
                sw.WriteLine($"Porcentaje de Asistencia: {porcentajeAsistencia}%");
                sw.WriteLine($"¿Cumple con el mínimo de asistencia? {(cumple ? "Sí" : "No")}");
            }
        }
    }
}

