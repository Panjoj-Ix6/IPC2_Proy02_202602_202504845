using System;
using System.Collections.Generic;
using System.Text;
using Catalogo.Dominio.Modelos;

namespace Catalogo.Dominio.Estructuras
{
    public class ArbolLibros
    {
        private NodoLibro raiz;

        public void Insertar(Libro libro)
        {
            raiz = InsertarRecursivo(raiz, libro);
        }

        private NodoLibro InsertarRecursivo(NodoLibro nodoActual, Libro libro)
        {
            if (nodoActual == null)
            {
                return new NodoLibro(libro);
            }

            if (libro.Isbn < nodoActual.Dato.Isbn)
            {
                nodoActual.Izquierdo = InsertarRecursivo(nodoActual.Izquierdo, libro);
            }
            else if (libro.Isbn > nodoActual.Dato.Isbn)
            {
                nodoActual.Derecho = InsertarRecursivo(nodoActual.Derecho, libro);
            }
            else
            {
                throw new InvalidOperationException($"Ya existe un libro con ISBN {libro.Isbn}.");
            }

            return nodoActual;
        }

        public Libro Buscar(long isbn)
        {
            NodoLibro actual = raiz;
            while (actual != null)
            {
                if (isbn == actual.Dato.Isbn)
                {
                    return actual.Dato;
                }
                else if (isbn < actual.Dato.Isbn)
                {
                    actual = actual.Izquierdo;
                }
                else
                {
                    actual = actual.Derecho;
                }
            }
            return null;
        }
    }
}
