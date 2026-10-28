namespace AcademiaTrack;

class NodoAVL
{
    public Estudiante Alumno;
    public NodoAVL Izquierda;
    public NodoAVL Derecha;
    public NodoAVL Padre;
    public int Altura;

    public NodoAVL(Estudiante alumno)
    {
        Alumno = alumno;
        Altura = 1;
    }
}

class ArbolAVL
{
    private NodoAVL raiz;

    private int Altura(NodoAVL nodo)
    {
        if (nodo == null)
            return 0;
        return nodo.Altura;
    }

    private void ActualizarAltura(NodoAVL nodo)
    {
        nodo.Altura = 1 + Math.Max(Altura(nodo.Izquierda), Altura(nodo.Derecha));
    }

    private int Balance(NodoAVL nodo)
    {
        if (nodo == null)
            return 0;
        return Altura(nodo.Izquierda) - Altura(nodo.Derecha);
    }

    private NodoAVL GirarIzquierda(NodoAVL nodo)
    {
        NodoAVL nuevoPadre = nodo.Derecha;
        NodoAVL ramaCentral = nuevoPadre.Izquierda;
        NodoAVL padreAnterior = nodo.Padre;

        nuevoPadre.Padre = padreAnterior;
        if (padreAnterior == null)
            raiz = nuevoPadre;
        else if (padreAnterior.Izquierda == nodo)
            padreAnterior.Izquierda = nuevoPadre;
        else
            padreAnterior.Derecha = nuevoPadre;

        nuevoPadre.Izquierda = nodo;
        nodo.Padre = nuevoPadre;
        nodo.Derecha = ramaCentral;
        if (ramaCentral != null)
            ramaCentral.Padre = nodo;

        ActualizarAltura(nodo);
        ActualizarAltura(nuevoPadre);
        return nuevoPadre;
    }

    private NodoAVL GirarDerecha(NodoAVL nodo)
    {
        NodoAVL nuevoPadre = nodo.Izquierda;
        NodoAVL ramaCentral = nuevoPadre.Derecha;
        NodoAVL padreAnterior = nodo.Padre;

        nuevoPadre.Padre = padreAnterior;
        if (padreAnterior == null)
            raiz = nuevoPadre;
        else if (padreAnterior.Izquierda == nodo)
            padreAnterior.Izquierda = nuevoPadre;
        else
            padreAnterior.Derecha = nuevoPadre;

        nuevoPadre.Derecha = nodo;
        nodo.Padre = nuevoPadre;
        nodo.Izquierda = ramaCentral;
        if (ramaCentral != null)
            ramaCentral.Padre = nodo;

        ActualizarAltura(nodo);
        ActualizarAltura(nuevoPadre);
        return nuevoPadre;
    }

    public bool Insertar(Estudiante alumno)
    {
        NodoAVL nuevo = new NodoAVL(alumno);

        if (raiz == null)
        {
            raiz = nuevo;
            return true;
        }

        NodoAVL actual = raiz;
        NodoAVL padre = null;

        while (actual != null)
        {
            padre = actual;
            if (alumno.Codigo == actual.Alumno.Codigo)
                return false;

            if (alumno.Codigo < actual.Alumno.Codigo)
                actual = actual.Izquierda;
            else
                actual = actual.Derecha;
        }

        nuevo.Padre = padre;
        if (alumno.Codigo < padre.Alumno.Codigo)
            padre.Izquierda = nuevo;
        else
            padre.Derecha = nuevo;

        NodoAVL revisar = padre;
        while (revisar != null)
        {
            ActualizarAltura(revisar);
            int diferencia = Balance(revisar);

            if (diferencia > 1)
            {
                if (Balance(revisar.Izquierda) < 0)
                    GirarIzquierda(revisar.Izquierda);
                revisar = GirarDerecha(revisar);
            }
            else if (diferencia < -1)
            {
                if (Balance(revisar.Derecha) > 0)
                    GirarDerecha(revisar.Derecha);
                revisar = GirarIzquierda(revisar);
            }

            revisar = revisar.Padre;
        }
        return true;
    }

    public Estudiante Buscar(int codigoAlumno)
    {
        NodoAVL actual = raiz;

        while (actual != null)
        {
            if (codigoAlumno == actual.Alumno.Codigo)
                return actual.Alumno;

            if (codigoAlumno < actual.Alumno.Codigo)
                actual = actual.Izquierda;
            else
                actual = actual.Derecha;
        }
        return null;
    }

    public void MostrarEnOrden()
    {
        if (raiz == null)
        {
            Console.WriteLine("Todavía no hay estudiantes registrados.");
            return;
        }

        Stack<NodoAVL> camino = new Stack<NodoAVL>();
        NodoAVL actual = raiz;

        while (actual != null || camino.Count > 0)
        {
            while (actual != null)
            {
                camino.Push(actual);
                actual = actual.Izquierda;
            }

            actual = camino.Pop();
            Console.WriteLine(actual.Alumno.Codigo + " - " + actual.Alumno.Nombre +
                " | " + actual.Alumno.Carrera);
            actual = actual.Derecha;
        }
    }
}
