using System.Text;
using Catalogo.Dominio.Estructuras;

namespace Catalogo.Dominio.Visualizacion
{
    public static class GeneradorGraphviz
    {
        public static string GenerarDotLibros(ListaLibros libros, string tituloGrafo)
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine("digraph G {");
            texto.AppendLine("    rankdir=LR;");
            texto.AppendLine($"    label=\"{EscaparTexto(tituloGrafo)}\";");
            texto.AppendLine("    node [shape=box, style=filled, fillcolor=lightyellow];");

            NodoListaLibros nodo = libros.Primero;
            string? idAnterior = null;

            while (nodo != null)
            {
                string idActual = $"\"{nodo.Dato.Isbn}\"";
                texto.AppendLine($"    {idActual} [label=\"{EscaparTexto(nodo.Dato.Titulo)}\\n{nodo.Dato.Isbn}\"];");

                if (idAnterior != null)
                {
                    texto.AppendLine($"    {idAnterior} -> {idActual};");
                }

                idAnterior = idActual;
                nodo = nodo.Siguiente;
            }

            texto.AppendLine("}");
            return texto.ToString();
        }

        public static string GenerarDotJerarquiaCompleta(NodoCategoria primeraCategoriaPrincipal)
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine("digraph G {");
            texto.AppendLine("    node [shape=box, style=filled, fillcolor=lightyellow];");

            NodoCategoria actual = primeraCategoriaPrincipal;
            while (actual != null)
            {
                AgregarNodosCategoria(actual, texto);
                actual = actual.SiguienteHermana;
            }

            texto.AppendLine("}");
            return texto.ToString();
        }

        public static string GenerarDotDesdeCategoria(NodoCategoria categoria)
        {
            StringBuilder texto = new StringBuilder();
            texto.AppendLine("digraph G {");
            texto.AppendLine("    node [shape=box, style=filled, fillcolor=lightyellow];");
            AgregarNodosCategoria(categoria, texto);
            texto.AppendLine("}");
            return texto.ToString();
        }

        private static void AgregarNodosCategoria(NodoCategoria categoria, StringBuilder texto)
        {
            if (categoria == null)
            {
                return;
            }

            string idCategoria = $"\"cat_{categoria.Nombre}\"";
            texto.AppendLine($"    {idCategoria} [label=\"{EscaparTexto(categoria.Nombre)}\", fillcolor=lightblue];");

            NodoListaLibros nodoLibro = categoria.Libros.Primero;
            while (nodoLibro != null)
            {
                string idLibro = $"\"libro_{nodoLibro.Dato.Isbn}\"";
                texto.AppendLine($"    {idLibro} [label=\"{EscaparTexto(nodoLibro.Dato.Titulo)}\"];");
                texto.AppendLine($"    {idCategoria} -> {idLibro};");
                nodoLibro = nodoLibro.Siguiente;
            }

            NodoCategoria hijo = categoria.PrimeraSubcategoria;
            while (hijo != null)
            {
                string idHijo = $"\"cat_{hijo.Nombre}\"";
                texto.AppendLine($"    {idCategoria} -> {idHijo};");
                AgregarNodosCategoria(hijo, texto);
                hijo = hijo.SiguienteHermana;
            }
        }

        private static string EscaparTexto(string texto)
        {
            return texto.Replace("\"", "'");
        }
    }
}