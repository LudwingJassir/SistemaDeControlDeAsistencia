namespace SistemaAsistencia
{
    // Clase Universidad
    public class Universidad
    {
        // Clase anidada Estudiante
        public class Estudiante
        {
            // Propiedades
            public string Nombre { get; set; }
            public int SesionesTotales { get; set; }
            public int SesionesAsistidas { get; set; }

            // Clase anidada Asistencia
            public class Asistencia
            {
                // Método para calcular el porcentaje de asistencia
                public static double CalcularPorcentaje(int sesionesTotales, int sesionesAsistidas)
                {
                    return (double)sesionesAsistidas / sesionesTotales * 100;
                }

                // Método para verificar si cumple con el mínimo
                public static bool CumpleMinimo(double porcentaje)
                {
                    return porcentaje >= 75;
                }
            }
        }
    }
}
