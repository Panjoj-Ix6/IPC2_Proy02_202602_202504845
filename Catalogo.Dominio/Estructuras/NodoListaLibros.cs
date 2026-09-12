using Catalogo.Dominio.Modelos;

namespace Catalogo.Dominio.Estructuras
{
    public class NodoListaLibros
    {
        public Libro Dato { get; set; }
        public NodoListaLibros Siguiente { get; set; }

        public NodoListaLibros(Libro dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}