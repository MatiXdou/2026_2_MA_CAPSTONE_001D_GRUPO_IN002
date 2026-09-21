let detalleCompra = [];


// ==========================================
// AGREGAR PRODUCTO
// ==========================================
document.getElementById("btnAgregar").addEventListener("click", function () {

    const combo = document.getElementById("producto");
    const cantidadInput = document.getElementById("cantidad");

    const opcion = combo.options[combo.selectedIndex];

    const idProducto = parseInt(combo.value);
    const cantidad = parseInt(cantidadInput.value);

    if (!idProducto) {
        alert("Seleccione un producto.");
        return;
    }

    if (!cantidad || cantidad <= 0) {
        alert("Ingrese una cantidad válida.");
        return;
    }

    const nombre = opcion.text;
    const precioNormal = parseInt(opcion.dataset.precioNormal);
    const precioMayorista = parseInt(opcion.dataset.precioMayorista);
    const stock = parseInt(opcion.dataset.stock);


    // Buscar si ya existe
    const productoExistente =
        detalleCompra.find(x => x.idProducto === idProducto);


    // Cantidad que quedaría después de agregar
    const cantidadFinal = productoExistente
        ? productoExistente.cantidad + cantidad
        : cantidad;


    // Validar stock
    if (cantidadFinal > stock) {
        alert("La cantidad solicitada supera el stock disponible.");
        return;
    }


    if (productoExistente) {

        productoExistente.cantidad += cantidad;

    } else {

        detalleCompra.push({
            idProducto: idProducto,
            nombre: nombre,
            cantidad: cantidad,
            precioNormal: precioNormal,
            precioMayorista: precioMayorista,
            stock: stock
        });
    }


    mostrarDetalle();

    cantidadInput.value = 1;
});


// ==========================================
// MOSTRAR DETALLE
// ==========================================
function mostrarDetalle() {

    const tbody = document.getElementById("detalleCompra");

    tbody.innerHTML = "";


    if (detalleCompra.length === 0) {

        tbody.innerHTML = `
            <tr>
                <td colspan="5"
                    class="text-center text-muted">
                    No hay productos agregados.
                </td>
            </tr>
        `;

        document.getElementById("totalCompra").textContent = "$0";

        return;
    }


    // Total de unidades de toda la compra
    const totalUnidades = detalleCompra.reduce(
        (total, producto) => total + producto.cantidad,
        0
    );


    // 3 o más unidades = precio mayorista
    const aplicaMayorista = totalUnidades >= 3;

    let total = 0;


    detalleCompra.forEach(producto => {

        const precio = aplicaMayorista
            ? producto.precioMayorista
            : producto.precioNormal;

        const subtotal = producto.cantidad * precio;

        total += subtotal;


        tbody.innerHTML += `
            <tr>

                <td>
                    ${producto.nombre}
                </td>

                <td>
                    ${producto.cantidad}
                </td>

                <td>
                    $${precio.toLocaleString("es-CL")}
                </td>

                <td>
                    $${subtotal.toLocaleString("es-CL")}
                </td>

                <td class="text-end">

                    <button type="button"
                            class="btn btn-sm btn-outline-danger"
                            onclick="eliminarProducto(${producto.idProducto})">

                        🗑️

                    </button>

                </td>

            </tr>
        `;
    });


    document.getElementById("totalCompra").textContent =
        "$" + total.toLocaleString("es-CL");
}


// ==========================================
// ELIMINAR PRODUCTO
// ==========================================
function eliminarProducto(idProducto) {

    detalleCompra =
        detalleCompra.filter(
            x => x.idProducto !== idProducto
        );

    mostrarDetalle();
}


// ==========================================
// PAGAR COMPRA
// ==========================================
document.getElementById("btnPagar").addEventListener("click", async function () {

    if (detalleCompra.length === 0) {
        alert("Debe agregar al menos un producto.");
        return;
    }


    // Obtener total mostrado en pantalla
    const total = document.getElementById("totalCompra").textContent;


    // Confirmar compra
    const confirmar = confirm(
        `¿Confirma que desea realizar la compra por ${total}?`
    );

    if (!confirmar) {
        return;
    }


    // Solo enviamos producto y cantidad.
    // Los precios se calculan nuevamente en SQL Server.
    const detalle = detalleCompra.map(producto => ({
        idProducto: producto.idProducto,
        cantidad: producto.cantidad
    }));


    const compra = {
        detalle: detalle
    };


    try {

        const respuesta = await fetch("/Compra/Pagar", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(compra)
        });


        const resultado = await respuesta.json();


        if (resultado.ok) {

            alert(resultado.mensaje);

            window.location.href = "/HomeMayorista";

        } else {

            alert(resultado.mensaje);
        }

    } catch (error) {

        alert("Ocurrió un error al realizar la compra.");
        console.error(error);
    }
});