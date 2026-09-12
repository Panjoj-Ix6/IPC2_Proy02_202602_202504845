using Catalogo.Dominio.Modelos;
using Catalogo.Dominio.Estructuras;

ArbolLibros arbol = new ArbolLibros();

arbol.Insertar(new Libro(9780131103627, "The C Programming Language", "Kernighan y Ritchie", "Programacion"));
arbol.Insertar(new Libro(9780132350884, "Clean Code", "Robert Martin", "Programacion"));
arbol.Insertar(new Libro(9780201633610, "Design Patterns", "Gang of Four", "Programacion"));
arbol.Insertar(new Libro(9780262033848, "Introduction to Algorithms", "Cormen", "Algoritmos"));
arbol.Insertar(new Libro(9780134685991, "Effective Java", "Joshua Bloch", "Programacion"));

Console.WriteLine("Buscar ISBN 9780132350884:");
Libro encontrado = arbol.Buscar(9780132350884);
Console.WriteLine(encontrado != null ? encontrado.ToString() : "No encontrado");

Console.WriteLine();
Console.WriteLine("Libro con ISBN menor: " + arbol.ObtenerMinimo());
Console.WriteLine("Libro con ISBN mayor: " + arbol.ObtenerMaximo());

Console.WriteLine();
Console.WriteLine("Recorrido ascendente por ISBN:");
ListaLibros lista = arbol.RecorridoAscendente();
NodoListaLibros actual = lista.Primero;
while (actual != null)
{
    Console.WriteLine(actual.Dato);
    actual = actual.Siguiente;
}

Console.WriteLine();
Console.WriteLine("Eliminando ISBN 9780201633610...");
arbol.Eliminar(9780201633610);

Console.WriteLine("Recorrido ascendente despues de eliminar:");
lista = arbol.RecorridoAscendente();
actual = lista.Primero;
while (actual != null)
{
    Console.WriteLine(actual.Dato);
    actual = actual.Siguiente;
}