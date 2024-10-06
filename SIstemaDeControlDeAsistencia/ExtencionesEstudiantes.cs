using System;

namespace SistemaAsistencia
{
    // Método de extensión para mostrar la información del estudiante
    public static class ExtensionesEstudiante
    {
        public static void MostrarInformacion(this Universidad.Estudiante estudiante)
        {
            double porcentajeAsistencia = Universidad.Estudiante.Asistencia.CalcularPorcentaje(estudiante.SesionesTotales, estudiante.SesionesAsistidas);
            bool cumple = Universidad.Estudiante.Asistencia.CumpleMinimo(porcentajeAsistencia);

            Console.WriteLine($"Nombre: {estudiante.Nombre}");
            Console.WriteLine($"Sesiones Totales: {estudiante.SesionesTotales}");
            Console.WriteLine($"Sesiones Asistidas: {estudiante.SesionesAsistidas}");
            Console.WriteLine($"Porcentaje de Asistencia: {porcentajeAsistencia}%");
            Console.WriteLine($"¿Cumple con el mínimo de asistencia? {(cumple ? "Sí" : "No")}");
        }
    }
}
