document.addEventListener("DOMContentLoaded", function () {

    // ==========================================
    // PANELES
    // ==========================================
    const panelListadoProductos = document.getElementById("panelListadoProductos");
    const panelFormularioProducto = document.getElementById("panelFormularioProducto");


    // ==========================================
    // BOTONES
    // ==========================================
    const btnNuevoProducto = document.getElementById("btnNuevoProducto");
    const btnCancelarProducto = document.getElementById("btnCancelarProducto");


    // ==========================================
    // FORMULARIO
    // ==========================================
    const formProducto = document.getElementById("formProducto");


    // ==========================================
    // TABLA
    // ==========================================
    const cuerpoProductos = document.getElementById("cuerpoProductos");


    // ==========================================
    // CAMPOS
    // ==========================================
    const lblTituloFormulario = document.getElementById("lblTituloFormulario");
    const txtBuscarProducto = document.getElementById("txtBuscarProducto");
    const txtNombre = document.getElementById("txtNombre");
    const txtDescripcion = document.getElementById("txtDescripcion");
    const txtPrecioVenta = document.getElementById("txtPrecioVenta");
    const txtPrecioMayorista = document.getElementById("txtPrecioMayorista");
    const txtStock = document.getElementById("txtStock");
    const ddlEstado = document.getElementById("ddlEstado");


    // null = nuevo producto
    // fila = producto que se está editando
    let filaEditando = null;


    // ==========================================
    // NUEVO PRODUCTO
    // ==========================================
    btnNuevoProducto.addEventListener("click", function () {

        filaEditando = null;
        formProducto.reset();
        ddlEstado.value = "Activo";

        lblTituloFormulario.textContent = "Nuevo producto";

        panelListadoProductos.style.display = "none";
        panelFormularioProducto.style.display = "block";
    });


    // ==========================================
    // CANCELAR
    // ==========================================
    btnCancelarProducto.addEventListener("click", function () {

        formProducto.reset();
        filaEditando = null;

        panelFormularioProducto.style.display = "none";
        panelListadoProductos.style.display = "block";
    });


    // ==========================================
    // GUARDAR / MODIFICAR
    // ==========================================
    formProducto.addEventListener("submit", async function (event) {

        event.preventDefault();

        const valorNombre = txtNombre.value.trim();
        const valorDescripcion = txtDescripcion.value.trim();
        const valorPrecioVenta = parseInt(txtPrecioVenta.value);
        const valorPrecioMayorista = parseInt(txtPrecioMayorista.value);
        const valorStock = parseInt(txtStock.value);
        const valorEstado = ddlEstado.value;

        let claseEstado = "bg-success";

        if (valorEstado === "Inactivo") {
            claseEstado = "bg-secondary";
        }


        // ======================================
        // MODIFICAR PRODUCTO EXISTENTE
        // ======================================
        if (filaEditando !== null) {

            const idProducto =
                parseInt(filaEditando.cells[0].textContent);

            const producto = {
                idProducto: idProducto,
                nombre: valorNombre,
                descripcion: valorDescripcion,
                precioVenta: valorPrecioVenta,
                precioMayorista: valorPrecioMayorista,
                stock: valorStock,
                estado: valorEstado
            };


            const respuesta =
                await fetch("/Producto/Editar", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(producto)
                });


            if (!respuesta.ok) {

                alert("No fue posible modificar el producto.");
                return;
            }


            // Actualizar la fila visualmente
            const celdas = filaEditando.cells;

            celdas[1].textContent = valorNombre;
            celdas[2].textContent = valorDescripcion;
            celdas[3].textContent =
                "$" + valorPrecioVenta.toLocaleString("es-CL");

            celdas[4].textContent =
                "$" + valorPrecioMayorista.toLocaleString("es-CL");

            celdas[5].textContent = valorStock;

            celdas[6].innerHTML =
                `<span class="badge ${claseEstado}">${valorEstado}</span>`;

            alert("Producto modificado correctamente.");
        }


        // ======================================
        // NUEVO PRODUCTO
        // ======================================
        else {

            const producto = {
                nombre: valorNombre,
                descripcion: valorDescripcion,
                precioVenta: valorPrecioVenta,
                precioMayorista: valorPrecioMayorista,
                stock: valorStock,
                estado: valorEstado
            };


            const confirmar =
                confirm("¿Desea crear el producto " + valorNombre + "?");

            if (!confirmar) {
                return;
            }


            const respuesta =
                await fetch("/Producto/Crear", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(producto)
                });


            if (!respuesta.ok) {

                alert("No fue posible crear el producto.");
                return;
            }


            const resultado = await respuesta.json();


            // Agregar nueva fila a la tabla
            const nuevaFila = cuerpoProductos.insertRow();

            nuevaFila.innerHTML = `
                <td>${resultado.idProducto}</td>
                <td>${valorNombre}</td>
                <td>${valorDescripcion}</td>
                <td>$${valorPrecioVenta.toLocaleString("es-CL")}</td>
                <td>$${valorPrecioMayorista.toLocaleString("es-CL")}</td>
                <td>${valorStock}</td>
                <td>
                    <span class="badge ${claseEstado}">
                        ${valorEstado}
                    </span>
                </td>
                <td class="text-end">
                    <button type="button"
                            class="btn btn-sm btn-outline-primary btn-editar"
                            title="Editar">
                        ✏️
                    </button>
                </td>
            `;

            alert("Producto creado correctamente.");
        }


        formProducto.reset();
        filaEditando = null;

        panelFormularioProducto.style.display = "none";
        panelListadoProductos.style.display = "block";
    });


    // ==========================================
    // EDITAR
    // ==========================================
    cuerpoProductos.addEventListener("click", function (event) {

        const botonEditar = event.target.closest(".btn-editar");

        if (botonEditar) {

            filaEditando = botonEditar.closest("tr");

            const celdas = filaEditando.cells;

            txtNombre.value =
                celdas[1].textContent.trim();

            txtDescripcion.value =
                celdas[2].textContent.trim();

            txtPrecioVenta.value =
                celdas[3].textContent
                    .replace("$", "")
                    .replaceAll(".", "")
                    .replaceAll(",", "")
                    .trim();

            txtPrecioMayorista.value =
                celdas[4].textContent
                    .replace("$", "")
                    .replaceAll(".", "")
                    .replaceAll(",", "")
                    .trim();

            txtStock.value =
                celdas[5].textContent.trim();

            ddlEstado.value =
                celdas[6].textContent.trim();

            lblTituloFormulario.textContent =
                "Editar producto";

            panelListadoProductos.style.display = "none";
            panelFormularioProducto.style.display = "block";
        }
    });

    // ==========================================
    // BUSCAR PRODUCTO
    // ==========================================
    txtBuscarProducto.addEventListener("input", function () {

        const textoBuscar =
            txtBuscarProducto.value.toLowerCase().trim();

        const filas =
            cuerpoProductos.querySelectorAll("tr");

        filas.forEach(function (fila) {

            const contenidoFila =
                fila.textContent.toLowerCase();

            if (contenidoFila.includes(textoBuscar)) {
                fila.style.display = "";
            }
            else {
                fila.style.display = "none";
            }
        });
    });

});