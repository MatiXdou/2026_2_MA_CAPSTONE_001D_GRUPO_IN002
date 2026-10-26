// =========================================================
// INICIAR PANTALLA
// =========================================================

document.addEventListener("DOMContentLoaded", function () {

    limpiar();

});


// =========================================================
// BOTÓN BUSCAR
// =========================================================

document.getElementById("btnBuscar").addEventListener("click", function () {

    consultarVentas();

});


// =========================================================
// BOTÓN LIMPIAR
// =========================================================

document.getElementById("btnLimpiar").addEventListener("click", function () {

    limpiar();

});


// =========================================================
// LIMPIAR
// =========================================================

function limpiar() {

    const hoy = new Date();
    const fechaActual = hoy.getFullYear() + "-" + String(hoy.getMonth() + 1).padStart(2, "0") + "-" + String(hoy.getDate()).padStart(2, "0");

    document.getElementById("fechaDesde").value = fechaActual;
    document.getElementById("fechaHasta").value = fechaActual;

    document.getElementById("tablaVentas").innerHTML = "";

    document.getElementById("cantidadVentas").innerText =
        "0 ventas encontradas";

    document.getElementById("totalConfirmadas").innerText =
        "$0";

    document.getElementById("totalAnuladas").innerText =
        "$0";
}


// =========================================================
// CONSULTAR VENTAS
// =========================================================

function consultarVentas() {

    const fechaDesde = document.getElementById("fechaDesde").value;
    const fechaHasta = document.getElementById("fechaHasta").value;


    // Validar fechas obligatorias
    if (fechaDesde == "" || fechaHasta == "") {

        alert("Debe ingresar ambas fechas.");
        return;
    }


    // Validar rango de fechas
    if (fechaHasta < fechaDesde) {

        alert("La fecha Hasta no puede ser menor que la fecha Desde.");
        return;
    }


    fetch("/Venta/Consultar?fechaDesde=" + fechaDesde + "&fechaHasta=" + fechaHasta)

        .then(response => response.json())

        .then(data => {

            mostrarVentas(data);

        });
}


// =========================================================
// MOSTRAR VENTAS
// =========================================================

function mostrarVentas(data) {

    const tabla = document.getElementById("tablaVentas");

    tabla.innerHTML = "";

    let totalConfirmadas = 0;
    let totalAnuladas = 0;


    data.forEach(venta => {

        let botonAnular = "";


        // Venta confirmada
        if (venta.estado == "Confirmada") {

            totalConfirmadas += venta.total;

            botonAnular =
                `<button class="btn btn-sm btn-outline-danger"
                         onclick="anularVenta(${venta.idVenta})">
                    Anular
                 </button>`;
        }


        // Venta anulada
        if (venta.estado == "Anulada") {

            totalAnuladas += venta.total;
        }


        const opciones = {
            day: "2-digit",
            month: "2-digit",
            year: "numeric",
            hour: "2-digit",
            minute: "2-digit",
            hour12: false
        };

        const fecha = new Date(venta.fechaVenta)
            .toLocaleString("es-CL", opciones)
            .replace(",", " ") + " Hrs.";

        const fechaAnulacion =
            venta.estado === "Anulada" && venta.fechaAnulacion
                ? new Date(venta.fechaAnulacion)
                    .toLocaleString("es-CL", opciones)
                    .replace(",", " ") + " Hrs."
                : "";

        tabla.innerHTML +=
            `<tr>

                <td>${venta.idVenta}</td>

                <td>${fecha}</td>

                <td>${fechaAnulacion}</td>

                <td>${venta.cliente}</td>

                <td>${venta.estado}</td>

                <td>
                    $${venta.total.toLocaleString("es-CL")}
                </td>

                <td class="text-end">

                    <button class="btn btn-sm btn-outline-primary"
                            onclick="verDetalle(${venta.idVenta})">
                        Ver
                    </button>

                    ${botonAnular}

                </td>

            </tr>`;
    });


    document.getElementById("cantidadVentas").innerText =
        data.length + " ventas encontradas";


    document.getElementById("totalConfirmadas").innerText =
        "$" + totalConfirmadas.toLocaleString("es-CL");


    document.getElementById("totalAnuladas").innerText =
        "$" + totalAnuladas.toLocaleString("es-CL");
}


// =========================================================
// VER DETALLE
// =========================================================

function verDetalle(idVenta) {

    fetch("/Venta/Detalle?idVenta=" + idVenta)

        .then(response => response.json())

        .then(data => {

            const tabla = document.getElementById("tablaDetalle");

            tabla.innerHTML = "";

            let total = 0;


            data.forEach(detalle => {

                total += detalle.subtotal;


                tabla.innerHTML +=
                    `<tr>

                        <td>${detalle.producto}</td>

                        <td>${detalle.cantidad}</td>

                        <td>
                            $${detalle.precioUnitario.toLocaleString("es-CL")}
                        </td>

                        <td>
                            $${detalle.subtotal.toLocaleString("es-CL")}
                        </td>

                    </tr>`;
            });


            document.getElementById("detalleIdVenta").innerText =
                idVenta;


            document.getElementById("totalDetalle").innerText =
                "$" + total.toLocaleString("es-CL");


            const modal = new bootstrap.Modal(
                document.getElementById("modalDetalleVenta")
            );

            modal.show();

        });
}


// =========================================================
// ANULAR VENTA
// =========================================================

function anularVenta(idVenta) {

    const confirmar = confirm(
        "¿Está seguro que desea anular esta venta?\n\n" +
        "Los productos serán devueltos al stock."
    );


    if (confirmar == false) {
        return;
    }


    fetch("/Venta/Anular?idVenta=" + idVenta, {
        method: "POST"
    })

        .then(response => response.json())

        .then(data => {

            alert(data.mensaje);


            if (data.ok == true) {

                consultarVentas();
            }

        });
}