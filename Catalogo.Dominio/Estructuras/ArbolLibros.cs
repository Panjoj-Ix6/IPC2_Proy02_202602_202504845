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

            ActualizarAltura(nodoActual);
            int balance = FactorBalance(nodoActual);

            // Caso Izquierda-Izquierda
            if (balance > 1 && libro.Isbn < nodoActual.Izquierdo.Dato.Isbn)
            {
                return RotarDerecha(nodoActual);
            }

            // Caso Derecha-Derecha
            if (balance < -1 && libro.Isbn > nodoActual.Derecho.Dato.Isbn)
            {
                return RotarIzquierda(nodoActual);
            }

            // Caso Izquierda-Derecha
            if (balance > 1 && libro.Isbn > nodoActual.Izquierdo.Dato.Isbn)
            {
                nodoActual.Izquierdo = RotarIzquierda(nodoActual.Izquierdo);
                return RotarDerecha(nodoActual);
            }

            // Caso Derecha-Izquierda
            if (balance < -1 && libro.Isbn < nodoActual.Derecho.Dato.Isbn)
            {
                nodoActual.Derecho = RotarDerecha(nodoActual.Derecho);
                return RotarIzquierda(nodoActual);
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

        private int Altura(NodoLibro nodo)
        {
            return nodo == null ? 0 : nodo.Altura;
        }

        private int FactorBalance(NodoLibro nodo)
        {
            return nodo == null ? 0 : Altura(nodo.Izquierdo) - Altura(nodo.Derecho);
        }

        private void ActualizarAltura(NodoLibro nodo)
        {
            nodo.Altura = 1 + Math.Max(Altura(nodo.Izquierdo), Altura(nodo.Derecho));
        }

        private NodoLibro RotarDerecha(NodoLibro y)
        {
            NodoLibro x = y.Izquierdo;
            NodoLibro t2 = x.Derecho;

            x.Derecho = y;
            y.Izquierdo = t2;

            ActualizarAltura(y);
            ActualizarAltura(x);

            return x;
        }

        private NodoLibro RotarIzquierda(NodoLibro x)
        {
            NodoLibro y = x.Derecho;
            NodoLibro t2 = y.Izquierdo;

            y.Izquierdo = x;
            x.Derecho = t2;

            ActualizarAltura(x);
            ActualizarAltura(y);

            return y;
        }

        public void Eliminar(long isbn)
        {
            raiz = EliminarRecursivo(raiz, isbn);
        }

        private NodoLibro EliminarRecursivo(NodoLibro nodoActual, long isbn)
        {
            if (nodoActual == null)
            {
                return null;
            }

            if (isbn < nodoActual.Dato.Isbn)
            {
                nodoActual.Izquierdo = EliminarRecursivo(nodoActual.Izquierdo, isbn);
            }
            else if (isbn > nodoActual.Dato.Isbn)
            {
                nodoActual.Derecho = EliminarRecursivo(nodoActual.Derecho, isbn);
            }
            else
            {
                if (nodoActual.Izquierdo == null || nodoActual.Derecho == null)
                {
                    NodoLibro hijo = nodoActual.Izquierdo ?? nodoActual.Derecho;
                    nodoActual = hijo;
                }
                else
                {
                    NodoLibro sucesor = nodoActual.Derecho;
                    while (sucesor.Izquierdo != null)
                    {
                        sucesor = sucesor.Izquierdo;
                    }

                    nodoActual.Dato = sucesor.Dato;
                    nodoActual.Derecho = EliminarRecursivo(nodoActual.Derecho, sucesor.Dato.Isbn);
                }
            }

            if (nodoActual == null)
            {
                return null;
            }

            ActualizarAltura(nodoActual);
            int balance = FactorBalance(nodoActual);

            if (balance > 1 && FactorBalance(nodoActual.Izquierdo) >= 0)
            {
                return RotarDerecha(nodoActual);
            }

            if (balance > 1 && FactorBalance(nodoActual.Izquierdo) < 0)
            {
                nodoActual.Izquierdo = RotarIzquierda(nodoActual.Izquierdo);
                return RotarDerecha(nodoActual);
            }

            if (balance < -1 && FactorBalance(nodoActual.Derecho) <= 0)
            {
                return RotarIzquierda(nodoActual);
            }

            if (balance < -1 && FactorBalance(nodoActual.Derecho) > 0)
            {
                nodoActual.Derecho = RotarDerecha(nodoActual.Derecho);
                return RotarIzquierda(nodoActual);
            }

            return nodoActual;
        }
        public Libro ObtenerMinimo()
        {
            if (raiz == null)
            {
                return null;
            }

            NodoLibro actual = raiz;
            while (actual.Izquierdo != null)
            {
                actual = actual.Izquierdo;
            }
            return actual.Dato;
        }

        public Libro ObtenerMaximo()
        {
            if (raiz == null)
            {
                return null;
            }

            NodoLibro actual = raiz;
            while (actual.Derecho != null)
            {
                actual = actual.Derecho;
            }
            return actual.Dato;
        }

        public ListaLibros RecorridoAscendente()
        {
            ListaLibros resultado = new ListaLibros();
            RecorridoAscendenteRecursivo(raiz, resultado);
            return resultado;
        }

        private void RecorridoAscendenteRecursivo(NodoLibro nodoActual, ListaLibros resultado)
        {
            if (nodoActual == null)
            {
                return;
            }

            RecorridoAscendenteRecursivo(nodoActual.Izquierdo, resultado);
            resultado.AgregarFinal(nodoActual.Dato);
            RecorridoAscendenteRecursivo(nodoActual.Derecho, resultado);
        }
    }
}
