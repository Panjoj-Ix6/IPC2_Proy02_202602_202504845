namespace Catalogo.Dominio.Estructuras
{
    public class NodoCategoria
    {
        public string Nombre { get; set; }
        public NodoCategoria PrimeraSubcategoria { get; set; }
        public NodoCategoria SiguienteHermana { get; set; }
        public ListaLibros Libros { get; set; }

        public NodoCategoria(string nombre)
        {
            Nombre = nombre;
            PrimeraSubcategoria = null;
            SiguienteHermana = null;
            Libros = new ListaLibros();
        }

        public void AgregarSubcategoria(NodoCategoria nueva)
        {
            if (PrimeraSubcategoria == null ||
                string.Compare(nueva.Nombre, PrimeraSubcategoria.Nombre, StringComparison.OrdinalIgnoreCase) < 0)
            {
                nueva.SiguienteHermana = PrimeraSubcategoria;
                PrimeraSubcategoria = nueva;
                return;
            }

            NodoCategoria actual = PrimeraSubcategoria;
            while (actual.SiguienteHermana != null &&
                   string.Compare(actual.SiguienteHermana.Nombre, nueva.Nombre, StringComparison.OrdinalIgnoreCase) < 0)
            {
                actual = actual.SiguienteHermana;
            }

            nueva.SiguienteHermana = actual.SiguienteHermana;
            actual.SiguienteHermana = nueva;
        }
    }
}