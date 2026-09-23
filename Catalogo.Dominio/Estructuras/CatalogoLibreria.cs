using System;
using Catalogo.Dominio.Modelos;

namespace Catalogo.Dominio.Estructuras
{
    public class CatalogoLibreria
    {
        private ArbolLibros indiceLibros;
        private ArbolCategorias arbolCategorias;

        public CatalogoLibreria()
        {
            indiceLibros = new ArbolLibros();
            arbolCategorias = new ArbolCategorias();
        }

        public ArbolLibros IndiceLibros => indiceLibros;
        public ArbolCategorias Categorias => arbolCategorias;

        public void RegistrarCategoriaPrincipal(string nombre)
        {
            arbolCategorias.AgregarCategoriaPrincipal(nombre);
        }

        public void RegistrarSubcategoria(string nombre, string nombrePadre)
        {
            arbolCategorias.AgregarSubcategoria(nombre, nombrePadre);
        }

        public void RegistrarLibro(long isbn, string titulo, string autor, string nombreCategoria)
        {
            NodoCategoria categoria = arbolCategorias.BuscarPorNombre(nombreCategoria);
            if (categoria == null)
            {
                throw new InvalidOperationException(
                    $"No existe la categoría '{nombreCategoria}' para el libro con ISBN {isbn}.");
            }

            Libro libro = new Libro(isbn, titulo, autor, nombreCategoria);

            indiceLibros.Insertar(libro);
            categoria.Libros.AgregarFinal(libro);
        }

        public bool EliminarLibro(long isbn)
        {
            Libro libro = indiceLibros.Buscar(isbn);

            if (libro == null)
            {
                return false;
            }

            NodoCategoria categoria = arbolCategorias.BuscarPorNombre(libro.NombreCategoria);

            indiceLibros.Eliminar(isbn);

            if (categoria != null)
            {
                categoria.Libros.Eliminar(isbn);
            }

            return true;
        }

        public ListaLibros ObtenerLibrosDeCategoriaOrdenados(string nombreCategoria)
        {
            ListaLibros todosOrdenados = indiceLibros.RecorridoAscendente();
            ListaLibros resultado = new ListaLibros();

            NodoListaLibros nodo = todosOrdenados.Primero;
            while (nodo != null)
            {
                if (string.Equals(nodo.Dato.NombreCategoria, nombreCategoria, StringComparison.OrdinalIgnoreCase))
                {
                    resultado.AgregarFinal(nodo.Dato);
                }
                nodo = nodo.Siguiente;
            }

            return resultado;
        }
        public void MostrarOrganizacion()
        {
            arbolCategorias.MostrarOrganizacion();
        }
    }
}