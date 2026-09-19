namespace Catalogo.Dominio.Estructuras
{
    public class NodoIndiceCategoria
    {
        public string Nombre { get; set; }
        public NodoCategoria Categoria { get; set; }
        public NodoIndiceCategoria Izquierdo { get; set; }
        public NodoIndiceCategoria Derecho { get; set; }

        public NodoIndiceCategoria(NodoCategoria categoria)
        {
            Nombre = categoria.Nombre;
            Categoria = categoria;
            Izquierdo = null;
            Derecho = null;
        }
    }
}