using System.Globalization;

namespace AcademiaTrack;

class SistemaAcademico
{
    private ArbolAVL estudiantes = new ArbolAVL();
    private Materia[] catalogo = new Materia[30];
    private int cantidadMaterias = 0;
    private ColaSolicitudes filaInscripciones = new ColaSolicitudes();
    private PilaInscripciones ultimasInscripciones = new PilaInscripciones();

    public SistemaAcademico()
    {
        // Algunas materias de ejemplo para probar el sistema.
        GuardarMateria(new Materia(101, "Programacion I", 6));
        GuardarMateria(new Materia(102, "Calculo I", 5));
        GuardarMateria(new Materia(103, "Base de Datos", 5));
        GuardarMateria(new Materia(104, "Redes", 4));
        GuardarMateria(new Materia(105, "Estructura de Datos", 6));
    }



    private void MostrarColor(string mensaje, ConsoleColor color)
    {
        ConsoleColor colorAnterior = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(mensaje);
        Console.ForegroundColor = colorAnterior;
    }

    public void Iniciar()
    {
        string opcion;
        do
        {
            MostrarMenu();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Elige una opción: ");
            Console.ResetColor();
            opcion = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine();

            switch (opcion)
            {
                case "1": RegistrarEstudiante(); break;
                case "2": BuscarEstudiante(); break;
                case "3": estudiantes.MostrarEnOrden(); break;
                case "4": MostrarCatalogo(); break;
                case "5": RegistrarMateria(); break;
                case "6": SolicitarInscripcion(); break;
                case "7": ProcesarFila(); break;
                case "8": DeshacerInscripcion(); break;
                case "9": RegistrarNota(); break;
                case "10": VerPromedio(); break;
                case "11": CrearSemestre(); break;
                case "12": VerHistorial(); break;
                case "0": MostrarColor("Saliendo de AcademiaTrack...", ConsoleColor.Cyan); break;
                default: MostrarColor("Esa opción no existe.", ConsoleColor.Red); break;
            }

            if (opcion != "0")
            {
                Console.WriteLine("\nPresiona Enter para volver al menú...");
                Console.ReadLine();
            }
        } while (opcion != "0");
    }

    private void MostrarMenu()
    {
        MostrarColor("\n========== ACADEMIATRACK ==========", ConsoleColor.Cyan);
        MostrarColor("--- ESTUDIANTES ---", ConsoleColor.Yellow);
        Console.WriteLine("1. Registrar estudiante");
        Console.WriteLine("2. Buscar estudiante por código");
        Console.WriteLine("3. Mostrar estudiantes");
        MostrarColor("--- MATERIAS E INSCRIPCIONES ---", ConsoleColor.Yellow);
        Console.WriteLine("4. Ver catálogo de materias");
        Console.WriteLine("5. Registrar una materia");
        Console.WriteLine("6. Solicitar inscripción (cola)");
        Console.WriteLine("7. Procesar siguiente inscripción");
        Console.WriteLine("8. Deshacer última inscripción (pila)");
        MostrarColor("--- NOTAS E HISTORIAL ---", ConsoleColor.Yellow);
        Console.WriteLine("9. Registrar o cambiar nota");
        Console.WriteLine("10. Ver promedio ponderado");
        Console.WriteLine("11. Pasar al siguiente semestre");
        Console.WriteLine("12. Ver historial académico");
        MostrarColor("0. Salir", ConsoleColor.Green);
        MostrarColor("Solicitudes pendientes: " + filaInscripciones.Cantidad, ConsoleColor.Yellow);
        Console.WriteLine("===================================");
    }

    private int LeerEntero(string mensaje)
    {
        int numero;
        while (true)
        {
            Console.Write(mensaje);
            string texto = Console.ReadLine();
            if (int.TryParse(texto, out numero) && numero > 0)
                return numero;

            MostrarColor("Ingresa un número entero mayor a cero.", ConsoleColor.Red);
        }
    }

    private string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string texto = (Console.ReadLine() ?? "").Trim();
            if (texto.Length > 0)
                return texto;

            MostrarColor("Este dato no puede quedar vacío.", ConsoleColor.Red);
        }
    }

    private double LeerNota()
    {
        while (true)
        {
            Console.Write("Nota (0 a 100): ");
            string texto = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
            double nota;

            if (double.TryParse(texto, NumberStyles.Float,
                CultureInfo.InvariantCulture, out nota) && nota >= 0 && nota <= 100)
                return nota;

            MostrarColor("Ingresa una nota entre 0 y 100.", ConsoleColor.Red);
        }
    }

    private Estudiante ElegirEstudiante()
    {
        int codigoAlumno = LeerEntero("Código del estudiante: ");
        Estudiante alumno = estudiantes.Buscar(codigoAlumno);

        if (alumno == null)
            MostrarColor("No se encontró un estudiante con ese código.", ConsoleColor.Red);

        return alumno;
    }

    private void RegistrarEstudiante()
    {
        MostrarColor("--- NUEVO ESTUDIANTE ---", ConsoleColor.Cyan);
        int codigoAlumno = LeerEntero("Código: ");

        if (estudiantes.Buscar(codigoAlumno) != null)
        {
            MostrarColor("Ese código ya está registrado.", ConsoleColor.Red);
            return;
        }

        string nombreAlumno = LeerTexto("Nombre completo: ");
        string carreraAlumno = LeerTexto("Carrera: ");

        Estudiante nuevoAlumno = new Estudiante(codigoAlumno, nombreAlumno, carreraAlumno);
        estudiantes.Insertar(nuevoAlumno);
        MostrarColor("Estudiante registrado. Se creó su semestre 1.", ConsoleColor.Green);
    }

    private void BuscarEstudiante()
    {
        MostrarColor("--- BUSCAR ESTUDIANTE ---", ConsoleColor.Cyan);
        Estudiante alumno = ElegirEstudiante();
        if (alumno == null)
            return;

        Console.WriteLine("Código: " + alumno.Codigo);
        Console.WriteLine("Nombre: " + alumno.Nombre);
        Console.WriteLine("Carrera: " + alumno.Carrera);
        Console.WriteLine("Semestre actual: " + alumno.SemestreActual.Numero);
        alumno.SemestreActual.Materias.Mostrar();
    }

    private Materia BuscarMateria(int codigoMateria)
    {
        for (int i = 0; i < cantidadMaterias; i++)
        {
            if (catalogo[i].Codigo == codigoMateria)
                return catalogo[i];
        }
        return null;
    }

    private bool GuardarMateria(Materia materia)
    {
        if (cantidadMaterias >= catalogo.Length || BuscarMateria(materia.Codigo) != null)
            return false;

        catalogo[cantidadMaterias] = materia;
        cantidadMaterias++;
        return true;
    }

    private void MostrarCatalogo()
    {
        MostrarColor("--- MATERIAS DISPONIBLES ---", ConsoleColor.Cyan);
        for (int i = 0; i < cantidadMaterias; i++)
        {
            Console.WriteLine(catalogo[i].Codigo + " - " + catalogo[i].Nombre +
                " | Horas: " + catalogo[i].Horas);
        }
    }

    private void RegistrarMateria()
    {
        MostrarColor("--- REGISTRAR MATERIA ---", ConsoleColor.Cyan);
        if (cantidadMaterias == catalogo.Length)
        {
            MostrarColor("El catálogo está lleno (30 materias como máximo).", ConsoleColor.Yellow);
            return;
        }

        int codigoMateria = LeerEntero("Código de la materia: ");
        if (BuscarMateria(codigoMateria) != null)
        {
            MostrarColor("Esa materia ya existe.", ConsoleColor.Red);
            return;
        }

        string nombreMateria = LeerTexto("Nombre de la materia: ");
        int horasMateria = LeerEntero("Carga horaria: ");
        GuardarMateria(new Materia(codigoMateria, nombreMateria, horasMateria));
        MostrarColor("Materia registrada correctamente.", ConsoleColor.Green);
    }

    private void SolicitarInscripcion()
    {
        MostrarColor("--- SOLICITAR INSCRIPCIÓN ---", ConsoleColor.Cyan);
        Estudiante alumno = ElegirEstudiante();
        if (alumno == null)
            return;

        MostrarCatalogo();
        int codigoMateria = LeerEntero("Código de la materia: ");
        Materia materia = BuscarMateria(codigoMateria);
        if (materia == null)
        {
            MostrarColor("Esa materia no está en el catálogo.", ConsoleColor.Red);
            return;
        }

        SolicitudInscripcion solicitud = new SolicitudInscripcion(
            alumno, materia, alumno.SemestreActual);
        filaInscripciones.Encolar(solicitud);
        MostrarColor("Solicitud agregada a la cola. Lugar en fila: " + filaInscripciones.Cantidad, ConsoleColor.Green);
    }

    private void ProcesarFila()
    {
        MostrarColor("--- PROCESAR INSCRIPCIÓN ---", ConsoleColor.Cyan);
        SolicitudInscripcion solicitud = filaInscripciones.Sacar();
        if (solicitud == null)
        {
            MostrarColor("No hay solicitudes pendientes.", ConsoleColor.Yellow);
            return;
        }

        bool agregada = solicitud.Semestre.Materias.Agregar(solicitud.Materia);
        if (!agregada)
        {
            MostrarColor("No se inscribió: " + solicitud.Alumno.Nombre +
                " ya tiene esa materia en el semestre " + solicitud.Semestre.Numero + ".", ConsoleColor.Red);
            return;
        }

        RegistroInscripcion registro = new RegistroInscripcion(
            solicitud.Alumno, solicitud.Materia, solicitud.Semestre);
        ultimasInscripciones.Apilar(registro);

        MostrarColor("Inscripción completada:", ConsoleColor.Green);
        Console.WriteLine(solicitud.Alumno.Nombre + " -> " + solicitud.Materia.Nombre +
            " (semestre " + solicitud.Semestre.Numero + ")");
    }

    private void DeshacerInscripcion()
    {
        MostrarColor("--- DESHACER INSCRIPCIÓN ---", ConsoleColor.Cyan);
        RegistroInscripcion registro = ultimasInscripciones.Sacar();
        if (registro == null)
        {
            MostrarColor("No hay inscripciones para deshacer.", ConsoleColor.Yellow);
            return;
        }

        registro.Semestre.Materias.Quitar(registro.Materia.Codigo);
        MostrarColor("Se quitó " + registro.Materia.Nombre + " de " +
            registro.Alumno.Nombre + " (semestre " + registro.Semestre.Numero + ").", ConsoleColor.Yellow);
    }

    private void RegistrarNota()
    {
        MostrarColor("--- REGISTRAR NOTA ---", ConsoleColor.Cyan);
        Estudiante alumno = ElegirEstudiante();
        if (alumno == null)
            return;

        Console.WriteLine("Materias del semestre " + alumno.SemestreActual.Numero + ":");
        alumno.SemestreActual.Materias.Mostrar();
        int codigoMateria = LeerEntero("Código de la materia inscrita: ");
        if (!alumno.SemestreActual.Materias.Contiene(codigoMateria))
        {
            MostrarColor("El estudiante no está inscrito en esa materia este semestre.", ConsoleColor.Red);
            return;
        }

        double nota = LeerNota();
        alumno.SemestreActual.Materias.RegistrarNota(codigoMateria, nota);
        MostrarColor("Nota guardada correctamente.", ConsoleColor.Green);
    }

    private void VerPromedio()
    {
        MostrarColor("--- PROMEDIO PONDERADO ---", ConsoleColor.Cyan);
        Estudiante alumno = ElegirEstudiante();
        if (alumno == null)
            return;

        Console.WriteLine(alumno.Nombre + " - Semestre " + alumno.SemestreActual.Numero);
        alumno.SemestreActual.Materias.Mostrar();

        if (alumno.SemestreActual.Materias.TieneNotas())
        {
            double promedio = alumno.SemestreActual.Materias.CalcularPromedio();
            MostrarColor("Promedio ponderado: " + promedio.ToString("0.00"), ConsoleColor.Green);
            Console.WriteLine("Se calcula con la nota y la carga horaria de cada materia calificada.");
        }
        else
        {
            MostrarColor("Todavía no hay notas para calcular el promedio.", ConsoleColor.Yellow);
        }
    }

    private void CrearSemestre()
    {
        MostrarColor("--- SIGUIENTE SEMESTRE ---", ConsoleColor.Cyan);
        Estudiante alumno = ElegirEstudiante();
        if (alumno == null)
            return;

        Semestre nuevo = alumno.Historial.AgregarSemestre();
        MostrarColor("Se creó el semestre " + nuevo.Numero + " para " + alumno.Nombre + ".", ConsoleColor.Green);
        Console.WriteLine("Las materias y notas anteriores quedan en su historial.");
    }

    private void VerHistorial()
    {
        MostrarColor("--- HISTORIAL ACADÉMICO ---", ConsoleColor.Cyan);
        Estudiante alumno = ElegirEstudiante();
        if (alumno == null)
            return;

        Console.WriteLine("Historial de " + alumno.Nombre);
        alumno.Historial.Navegar();
    }
}
