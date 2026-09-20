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

            if (raiz == null ||
                string.Compare(nueva.Nombre, raiz.Nombre, StringComparison.OrdinalIgnoreCase) < 0)
            {
                nueva.SiguienteHermana = raiz;
                raiz = nueva;
            }
            else
            {
                NodoCategoria actual = raiz;
                while (actual.SiguienteHermana != null &&
                       string.Compare(actual.SiguienteHermana.Nombre, nueva.Nombre, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    actual = actual.SiguienteHermana;
                }

                nueva.SiguienteHermana = actual.SiguienteHermana;
                actual.SiguienteHermana = nueva;
            }

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

        public void MostrarOrganizacion()
        {
            MostrarDesde(raiz, 0);
        }

        private void MostrarDesde(NodoCategoria categoria, int nivel)
        {
            if (categoria == null)
            {
                return;
            }

            string sangria = new string(' ', nivel * 2);
            Console.WriteLine($"{sangria}- {categoria.Nombre}");

            string sangriaLibro = new string(' ', (nivel + 1) * 2);
            NodoListaLibros nodoLibro = categoria.Libros.Primero;
            while (nodoLibro != null)
            {
                Console.WriteLine($"{sangriaLibro}* {nodoLibro.Dato}");
                nodoLibro = nodoLibro.Siguiente;
            }

            MostrarDesde(categoria.PrimeraSubcategoria, nivel + 1);
            MostrarDesde(categoria.SiguienteHermana, nivel);
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