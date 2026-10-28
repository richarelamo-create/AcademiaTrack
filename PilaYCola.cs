namespace AcademiaTrack;

// PILA: la última inscripción realizada es la primera que se deshace.
class NodoPila
{
    public RegistroInscripcion Dato;
    public NodoPila Siguiente;

    public NodoPila(RegistroInscripcion dato)
    {
        Dato = dato;
    }
}

class PilaInscripciones
{
    private NodoPila cima;

    public void Apilar(RegistroInscripcion registro)
    {
        NodoPila nuevo = new NodoPila(registro);
        nuevo.Siguiente = cima;
        cima = nuevo;
    }

    public RegistroInscripcion Sacar()
    {
        if (cima == null)
            return null;

        RegistroInscripcion dato = cima.Dato;
        cima = cima.Siguiente;
        return dato;
    }
}

// COLA: las solicitudes se atienden en el mismo orden en que llegan.
class NodoCola
{
    public SolicitudInscripcion Dato;
    public NodoCola Siguiente;

    public NodoCola(SolicitudInscripcion dato)
    {
        Dato = dato;
    }
}

class ColaSolicitudes
{
    private NodoCola primero;
    private NodoCola ultimo;
    public int Cantidad { get; private set; }

    public void Encolar(SolicitudInscripcion solicitud)
    {
        NodoCola nuevo = new NodoCola(solicitud);

        if (ultimo == null)
            primero = nuevo;
        else
            ultimo.Siguiente = nuevo;

        ultimo = nuevo;
        Cantidad++;
    }

    public SolicitudInscripcion Sacar()
    {
        if (primero == null)
            return null;

        SolicitudInscripcion dato = primero.Dato;
        primero = primero.Siguiente;

        if (primero == null)
            ultimo = null;

        Cantidad--;
        return dato;
    }
}
