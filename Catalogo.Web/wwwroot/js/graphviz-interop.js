window.renderizarGraphviz = async function (dotTexto, idContenedor) {
    const contenedor = document.getElementById(idContenedor);
    if (!contenedor) {
        return;
    }
    try {
        const instancia = await Viz.instance();
        const svg = instancia.renderSVGElement(dotTexto);
        contenedor.innerHTML = "";
        contenedor.appendChild(svg);
    } catch (error) {
        contenedor.innerHTML = "<p style='color:red'>Error al generar el grafo: " + error.message + "</p>";
    }
};