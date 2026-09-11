using System;
using System.Collections.Generic;
using System.Text;
using Catalogo.Dominio.Modelos;

namespace Catalogo.Dominio.Estructuras
{
    public class NodoLibro
    {
        public Libro Dato { get; set; }
        public NodoLibro Izquierdo { get; set; }
        public NodoLibro Derecho { get; set; }
        public int Altura { get; set; }

        public NodoLibro(Libro dato)
        {
            Dato = dato;
            Izquierdo = null;
            Derecho = null;
            Altura = 1;
        } }
}
