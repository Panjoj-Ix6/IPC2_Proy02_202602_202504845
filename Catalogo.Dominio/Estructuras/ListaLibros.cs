using Catalogo.Dominio.Modelos;

namespace Catalogo.Dominio.Estructuras
{
    public class ListaLibros
    {
        private NodoListaLibros primero;
        private NodoListaLibros ultimo;
        private int cantidad;

        public int Cantidad => cantidad;
        public NodoListaLibros Primero => primero;

        public void AgregarFinal(Libro libro)
        {
            NodoListaLibros nuevo = new NodoListaLibros(libro);

            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                ultimo.Siguiente = nuevo;
                ultimo = nuevo;
            }

            cantidad++;
        }
    }
}
