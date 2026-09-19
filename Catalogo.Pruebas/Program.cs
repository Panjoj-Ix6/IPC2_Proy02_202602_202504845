using Catalogo.Dominio.Estructuras;
using Catalogo.Dominio.Entrada;

CatalogoLibreria catalogo = new CatalogoLibreria();
CargadorXml cargador = new CargadorXml();

cargador.CargarArchivo("entrada.xml", catalogo);

Console.WriteLine("Buscar ISBN 9780262033848:");
var libro = catalogo.IndiceLibros.Buscar(9780262033848);
Console.WriteLine(libro != null ? libro.ToString() : "No encontrado");

Console.WriteLine();
Console.WriteLine("Libros de la categoria 'Programacion':");
NodoCategoria programacion = catalogo.Categorias.BuscarPorNombre("Programacion");
NodoListaLibros actual = programacion.Libros.Primero;
while (actual != null)
{
    Console.WriteLine(actual.Dato);
    actual = actual.Siguiente;
}

Console.WriteLine();
Console.WriteLine("Recorrido ascendente de todo el catalogo:");
ListaLibros todos = catalogo.IndiceLibros.RecorridoAscendente();
actual = todos.Primero;
while (actual != null)
{
    Console.WriteLine(actual.Dato);
    actual = actual.Siguiente;
}