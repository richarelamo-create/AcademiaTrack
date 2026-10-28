namespace AcademiaTrack;

// Cada semestre es un nodo de una lista doblemente enlazada.
class Semestre
{
    public int Numero;
    public ListaMaterias Materias;
    public Semestre Anterior;
    public Semestre Siguiente;

    public Semestre(int numero)
    {
        Numero = numero;
        Materias = new ListaMaterias();
        Anterior = null;
        Siguiente = null;
    }
}

class HistorialAcademico
{
    public Semestre Primero { get; private set; }
    public Semestre Ultimo { get; private set; }

    public Semestre AgregarSemestre()
    {
        int numero = 1;
        if (Ultimo != null)
            numero = Ultimo.Numero + 1;

        Semestre nuevo = new Semestre(numero);

        if (Primero == null)
        {
            Primero = nuevo;
            Ultimo = nuevo;
        }
        else
        {
            Ultimo.Siguiente = nuevo;
            nuevo.Anterior = Ultimo;
            Ultimo = nuevo;
        }
        return nuevo;
    }

    public void Navegar()
    {
        if (Ultimo == null)
        {
            Console.WriteLine("No hay historial para mostrar.");
            return;
        }

        Semestre actual = Ultimo;
        string opcion = "";

        while (opcion != "X")
        {
            Console.WriteLine("\n========== SEMESTRE " + actual.Numero + " ==========");
            actual.Materias.Mostrar();
            if (actual.Materias.TieneNotas())
                Console.WriteLine("Promedio: " + actual.Materias.CalcularPromedio().ToString("0.00"));
            else
                Console.WriteLine("Promedio: todavía no hay notas.");

            Console.WriteLine("A = anterior | S = siguiente | X = salir");
            Console.Write("Opción: ");
            opcion = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (opcion == "A")
            {
                if (actual.Anterior != null)
                    actual = actual.Anterior;
                else
                    Console.WriteLine("Ya estás en el primer semestre.");
            }
            else if (opcion == "S")
            {
                if (actual.Siguiente != null)
                    actual = actual.Siguiente;
                else
                    Console.WriteLine("Ya estás en el último semestre.");
            }
            else if (opcion != "X")
            {
                Console.WriteLine("Opción no válida.");
            }
        }
    }
}
