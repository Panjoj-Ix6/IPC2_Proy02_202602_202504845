using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Dominio.Modelos
{

        public class Libro
        {
            public long Isbn { get; private set; }
            public string Titulo { get; private set; }
            public string Autor { get; private set; }
            public string NombreCategoria { get; private set; }

            public Libro(long isbn, string titulo, string autor, string nombreCategoria)
            {
                Isbn = isbn;
                Titulo = titulo;
                Autor = autor;
                NombreCategoria = nombreCategoria;
            }

            public override string ToString()
            {
                return $"{Isbn} - {Titulo} ({Autor})";
            }
        }
 }

