namespace AcademiaTrack;

// Nodo de la lista simplemente enlazada.
class NodoMateria
{
    public Materia Materia;
    public double? Nota;
    public NodoMateria Siguiente;

    public NodoMateria(Materia materia)
    {
        Materia = materia;
        Nota = null;
        Siguiente = null;
    }
}

class ListaMaterias
{
    private NodoMateria primero;

    private NodoMateria BuscarNodo(int codigoMateria)
    {
        NodoMateria actual = primero;

        while (actual != null)
        {
            if (actual.Materia.Codigo == codigoMateria)
                return actual;

            actual = actual.Siguiente;
        }
        return null;
    }

    public bool Contiene(int codigoMateria)
    {
        return BuscarNodo(codigoMateria) != null;
    }

    public bool Agregar(Materia materia)
    {
        if (BuscarNodo(materia.Codigo) != null)
            return false;

        NodoMateria nuevo = new NodoMateria(materia);

        if (primero == null)
        {
            primero = nuevo;
        }
        else
        {
            NodoMateria actual = primero;
            while (actual.Siguiente != null)
                actual = actual.Siguiente;

            actual.Siguiente = nuevo;
        }
        return true;
    }

    public bool Quitar(int codigoMateria)
    {
        NodoMateria actual = primero;
        NodoMateria anterior = null;

        while (actual != null)
        {
            if (actual.Materia.Codigo == codigoMateria)
            {
                if (anterior == null)
                    primero = actual.Siguiente;
                else
                    anterior.Siguiente = actual.Siguiente;

                return true;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
        return false;
    }

    public bool RegistrarNota(int codigoMateria, double nota)
    {
        NodoMateria encontrada = BuscarNodo(codigoMateria);
        if (encontrada == null)
            return false;

        encontrada.Nota = nota;
        return true;
    }

    public bool TieneNotas()
    {
        NodoMateria actual = primero;
        while (actual != null)
        {
            if (actual.Nota.HasValue)
                return true;
            actual = actual.Siguiente;
        }
        return false;
    }

   
    // Solo se consideran las materias que ya tienen nota.
    public double CalcularPromedio()
    {
        double sumaPonderada = 0;
        int sumaHoras = 0;
        NodoMateria actual = primero;

        while (actual != null)
        {
            if (actual.Nota.HasValue)
            {
                sumaPonderada += actual.Nota.Value * actual.Materia.Horas;
                sumaHoras += actual.Materia.Horas;
            }
            actual = actual.Siguiente;
        }

        if (sumaHoras == 0)
            return 0;

        return sumaPonderada / sumaHoras;
    }

    public void Mostrar()
    {
        if (primero == null)
        {
            Console.WriteLine("  No hay materias inscritas.");
            return;
        }

        NodoMateria actual = primero;
        while (actual != null)
        {
            string textoNota = "Pendiente";
            if (actual.Nota.HasValue)
                textoNota = actual.Nota.Value.ToString("0.00");

            Console.WriteLine("  " + actual.Materia.Codigo + " - " +
                actual.Materia.Nombre + " | Horas: " + actual.Materia.Horas +
                " | Nota: " + textoNota);

            actual = actual.Siguiente;
        }
    }
}
