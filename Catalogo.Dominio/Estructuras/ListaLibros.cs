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
        public bool Eliminar(long isbn)
        {
            NodoListaLibros actual = primero;
            NodoListaLibros anterior = null;

            while (actual != null)
            {
                if (actual.Dato.Isbn == isbn)
                {
                    if (anterior == null)
                    {
                        primero = actual.Siguiente;
                    }
                    else
                    {
                        anterior.Siguiente = actual.Siguiente;
                    }

                    if (actual == ultimo)
                    {
                        ultimo = anterior;
                    }

                    cantidad--;
                    return true;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            return false;
        }
    }
}
