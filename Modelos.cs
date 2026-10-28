namespace AcademiaTrack;

// Datos que guardamos de cada materia.
class Materia
{
    public int Codigo;
    public string Nombre;
    public int Horas;

    public Materia(int codigo, string nombre, int horas)
    {
        Codigo = codigo;
        Nombre = nombre;
        Horas = horas;
    }
}

// Cada estudiante tiene su propio historial de semestres.
class Estudiante
{
    public int Codigo;
    public string Nombre;
    public string Carrera;
    public HistorialAcademico Historial;

    public Estudiante(int codigo, string nombre, string carrera)
    {
        Codigo = codigo;
        Nombre = nombre;
        Carrera = carrera;
        Historial = new HistorialAcademico();
        Historial.AgregarSemestre();
    }

    public Semestre SemestreActual
    {
        get { return Historial.Ultimo; }
    }
}

// Lo que queda esperando en la cola de inscripciones.
class SolicitudInscripcion
{
    public Estudiante Alumno;
    public Materia Materia;
    public Semestre Semestre;

    public SolicitudInscripcion(Estudiante alumno, Materia materia, Semestre semestre)
    {
        Alumno = alumno;
        Materia = materia;
        Semestre = semestre;
    }
}

// Se guarda en la pila para poder deshacer una inscripción.
class RegistroInscripcion
{
    public Estudiante Alumno;
    public Materia Materia;
    public Semestre Semestre;

    public RegistroInscripcion(Estudiante alumno, Materia materia, Semestre semestre)
    {
        Alumno = alumno;
        Materia = materia;
        Semestre = semestre;
    }
}
