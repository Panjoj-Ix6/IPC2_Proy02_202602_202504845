using System;
using System.Xml.Linq;
using Catalogo.Dominio.Estructuras;

namespace Catalogo.Dominio.Entrada
{
    public class CargadorXml
    {
        public void CargarArchivo(string rutaArchivo, CatalogoLibreria catalogo)
        {
            XDocument documento = XDocument.Load(rutaArchivo);
            XElement raiz = documento.Root;

            XElement listaCategorias = raiz.Element("listaCategorias");
            if (listaCategorias != null)
            {
                foreach (XElement categoriaXml in listaCategorias.Elements("categoria"))
                {
                    string nombre = categoriaXml.Value.Trim();
                    XAttribute atributoPadre = categoriaXml.Attribute("padre");

                    if (atributoPadre == null)
                    {
                        catalogo.RegistrarCategoriaPrincipal(nombre);
                    }
                    else
                    {
                        catalogo.RegistrarSubcategoria(nombre, atributoPadre.Value);
                    }
                }
            }

            XElement listaLibros = raiz.Element("listaLibros");
            if (listaLibros != null)
            {
                foreach (XElement libroXml in listaLibros.Elements("libro"))
                {
                    long isbn = long.Parse(libroXml.Element("ISBN").Value.Trim());
                    string titulo = libroXml.Element("titulo").Value.Trim();
                    string autor = libroXml.Element("autor").Value.Trim();
                    string nombreCategoria = libroXml.Element("categoria").Value.Trim();

                    catalogo.RegistrarLibro(isbn, titulo, autor, nombreCategoria);
                }
            }
        }
    }
}