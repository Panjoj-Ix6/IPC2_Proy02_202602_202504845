using System;

namespace Catalogo.Dominio.Estructuras
{
    public class ArbolCategorias
    {
        private NodoCategoria raiz;
        private NodoIndiceCategoria indiceRaiz;

        public NodoCategoria Raiz => raiz;

        public void AgregarCategoriaPrincipal(string nombre)
        {
            if (BuscarPorNombre(nombre) != null)
            {
                throw new InvalidOperationException($"Ya existe una categoría llamada '{nombre}'.");
            }

            NodoCategoria nueva = new NodoCategoria(nombre);
            raiz = nueva;
            indiceRaiz = InsertarEnIndice(indiceRaiz, nueva);
        }

        public void AgregarSubcategoria(string nombre, string nombrePadre)
        {
            NodoCategoria padre = BuscarPorNombre(nombrePadre);
            if (padre == null)
            {
                throw new InvalidOperationException($"No existe la categoría padre '{nombrePadre}'.");
            }

            if (BuscarPorNombre(nombre) != null)
            {
                throw new InvalidOperationException($"Ya existe una categoría llamada '{nombre}'.");
            }

            NodoCategoria nueva = new NodoCategoria(nombre);
            padre.AgregarSubcategoria(nueva);
            indiceRaiz = InsertarEnIndice(indiceRaiz, nueva);
        }

        public NodoCategoria BuscarPorNombre(string nombre)
        {
            NodoIndiceCategoria actual = indiceRaiz;
            while (actual != null)
            {
                int comparacion = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);
                if (comparacion == 0)
                {
                    return actual.Categoria;
                }
                actual = comparacion < 0 ? actual.Izquierdo : actual.Derecho;
            }
            return null;
        }

        private NodoIndiceCategoria InsertarEnIndice(NodoIndiceCategoria nodoActual, NodoCategoria categoria)
        {
            if (nodoActual == null)
            {
                return new NodoIndiceCategoria(categoria);
            }

            if (string.Compare(categoria.Nombre, nodoActual.Nombre, StringComparison.OrdinalIgnoreCase) < 0)
            {
                nodoActual.Izquierdo = InsertarEnIndice(nodoActual.Izquierdo, categoria);
            }
            else
            {
                nodoActual.Derecho = InsertarEnIndice(nodoActual.Derecho, categoria);
            }

            return nodoActual;
        }
    }
}