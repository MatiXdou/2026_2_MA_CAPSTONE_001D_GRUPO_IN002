document.addEventListener("DOMContentLoaded", function () {

    // ==========================================
    // PANELES
    // ==========================================
    const panelListadoEmpresas = document.getElementById("panelListadoEmpresas");
    const panelFormularioEmpresa = document.getElementById("panelFormularioEmpresa");


    // ==========================================
    // BOTONES
    // ==========================================
    const btnNuevaEmpresa =
        document.getElementById("btnNuevaEmpresa");

    const btnCancelarEmpresa =
        document.getElementById("btnCancelarEmpresa");


    // ==========================================
    // FORMULARIO
    // ==========================================
    const formEmpresa =
        document.getElementById("formEmpresa");


    // ==========================================
    // TABLA
    // ==========================================
    const cuerpoEmpresas =
        document.getElementById("cuerpoEmpresas");

    const lblCantidadEmpresas =
        document.getElementById("lblCantidadEmpresas");


    // ==========================================
    // CAMPOS
    // ==========================================
    const lblTituloFormulario =
        document.getElementById("lblTituloFormulario");

    const txtBuscarEmpresa =
        document.getElementById("txtBuscarEmpresa");

    const txtRut =
        document.getElementById("txtRut");

    const txtRazonSocial =
        document.getElementById("txtRazonSocial");

    const txtEmail =
        document.getElementById("txtEmail");

    const txtTelefono =
        document.getElementById("txtTelefono");

    const txtDireccion =
        document.getElementById("txtDireccion");

    const txtNombreAdministrador =
        document.getElementById("txtNombreAdministrador");

    const txtEmailAdministrador =
        document.getElementById("txtEmailAdministrador");

    const ddlEstado =
        document.getElementById("ddlEstado");


    // null = nueva empresa
    // fila = empresa que se está editando
    let filaEditando = null;


    // ==========================================
    // NUEVA EMPRESA
    // ==========================================
    btnNuevaEmpresa.addEventListener("click", function () {

        filaEditando = null;
        formEmpresa.reset();
        ddlEstado.value = "Activa";
        lblTituloFormulario.textContent = "Nueva empresa";
        panelListadoEmpresas.style.display = "none";
        panelFormularioEmpresa.style.display = "block";

    });


    // ==========================================
    // CANCELAR
    // ==========================================
    btnCancelarEmpresa.addEventListener("click", function () {

        formEmpresa.reset();
        filaEditando = null;
        panelFormularioEmpresa.style.display = "none";
        panelListadoEmpresas.style.display = "block";

    });


    // ==========================================
    // GUARDAR / MODIFICAR
    // ==========================================
    formEmpresa.addEventListener("submit", async function (event) {

        event.preventDefault();

        const valorRut =
            txtRut.value.trim();

        const valorRazonSocial =
            txtRazonSocial.value.trim();

        const valorEmail =
            txtEmail.value.trim();

        const valorTelefono =
            txtTelefono.value.trim();

        const valorDireccion =
            txtDireccion.value.trim();

        const valorNombreAdministrador =
            txtNombreAdministrador.value.trim();

        const valorEmailAdministrador =
            txtEmailAdministrador.value.trim();

        const valorEstado =
            ddlEstado.value;


        let claseEstado = "bg-success";

        if (valorEstado === "Inactiva") {
            claseEstado = "bg-secondary";
        }


        // ======================================
        // MODIFICAR EMPRESA EXISTENTE
        // ======================================
        if (filaEditando !== null) {

            const idEmpresa =
                parseInt(filaEditando.cells[0].textContent);

            const empresa = {
                idEmpresa: idEmpresa,
                rut: valorRut,
                razonSocial: valorRazonSocial,
                EmailCorporativo: valorEmail,
                telefono: valorTelefono,
                direccion: valorDireccion,
                estado: valorEstado === "Activa",
                nombreAdministrador: valorNombreAdministrador,
                emailAdministrador: valorEmailAdministrador
            };


            const respuesta =
                await fetch("/Empresa/Modificar", {

                    method: "POST",

                    headers: {
                        "Content-Type": "application/json"
                    },

                    body: JSON.stringify(empresa)

                });


            if (!respuesta.ok) {

                alert("No fue posible modificar la empresa.");

                return;
            }


            // Actualizar la fila visualmente
            const celdas =
                filaEditando.cells;

            celdas[1].textContent =
                valorRut;

            celdas[2].textContent =
                valorRazonSocial;

            celdas[3].textContent =
                valorEmail;

            celdas[4].textContent =
                valorTelefono;

            celdas[5].textContent =
                valorDireccion;

            celdas[6].innerHTML = `
                <span class="badge ${claseEstado}">
                    ${valorEstado}
                </span>
            `;

            celdas[8].textContent =
                valorNombreAdministrador;

            celdas[9].textContent =
                valorEmailAdministrador;


            alert("Empresa modificada correctamente.");

        }


        // ======================================
        // NUEVA EMPRESA
        // ======================================
        else {

            const empresa = {
                rut: valorRut,
                razonSocial: valorRazonSocial,
                EmailCorporativo: valorEmail,
                telefono: valorTelefono,
                direccion: valorDireccion,
                estado: valorEstado === "Activa",
                nombreAdministrador: valorNombreAdministrador,
                emailAdministrador: valorEmailAdministrador
            };

            const confirmar = confirm(
                "¿Desea crear la empresa " +
                valorRazonSocial +
                "?"
            );

            if (!confirmar) {
                return;
            }

            const respuesta =
                await fetch("/Empresa/Crear", {

                    method: "POST",

                    headers: {
                        "Content-Type": "application/json"
                    },

                    body: JSON.stringify(empresa)

                });


            if (!respuesta.ok) {

                alert("No fue posible crear la empresa.");

                return;
            }


            alert("Empresa creada correctamente.");

            window.location.reload();
        }


        //actualizarCantidadEmpresas();

        formEmpresa.reset();

        filaEditando = null;

        panelFormularioEmpresa.style.display =
            "none";

        panelListadoEmpresas.style.display =
            "block";

    });


    // ==========================================
    // EDITAR Y ELIMINAR
    // ==========================================
    cuerpoEmpresas.addEventListener("click", async function (event) {

        const botonEditar =
            event.target.closest(".btn-editar");

        const botonEliminar =
            event.target.closest(".btn-eliminar");


        // ======================================
        // EDITAR
        // ======================================
        if (botonEditar) {

            filaEditando =
                botonEditar.closest("tr");

            const celdas =
                filaEditando.cells;


            txtRut.value =
                celdas[1].textContent.trim();

            txtRazonSocial.value =
                celdas[2].textContent.trim();

            txtEmail.value =
                celdas[3].textContent.trim();

            txtTelefono.value =
                celdas[4].textContent.trim();

            txtDireccion.value =
                celdas[5].textContent.trim();

            ddlEstado.value =
                celdas[6].textContent.trim();




            txtNombreAdministrador.value =
                celdas[8].textContent.trim();

            txtEmailAdministrador.value =
                celdas[9].textContent.trim();


            lblTituloFormulario.textContent =
                "Editar empresa";

            panelListadoEmpresas.style.display =
                "none";

            panelFormularioEmpresa.style.display =
                "block";
        }

        // ======================================
        // ELIMINAR
        // ======================================
        if (botonEliminar) {

            const fila =
                botonEliminar.closest("tr");

            const idEmpresa =
                parseInt(fila.cells[0].textContent);

            const razonSocialEmpresa =
                fila.cells[2].textContent.trim();


            const confirmar = confirm(
                "¿Desea eliminar la empresa " +
                razonSocialEmpresa +
                "?"
            );


            if (confirmar) {

                const respuesta =
                    await fetch("/Empresa/Eliminar", {

                        method: "POST",

                        headers: {
                            "Content-Type": "application/json"
                        },

                        body: JSON.stringify(idEmpresa)

                    });


                if (!respuesta.ok) {

                    alert("No fue posible eliminar la empresa.");

                    return;
                }


                fila.remove();

                actualizarCantidadEmpresas();

                alert("Empresa eliminada correctamente.");
            }
        }

    });


    // ==========================================
    // BUSCAR EMPRESA
    // ==========================================
    txtBuscarEmpresa.addEventListener("input", function () {

        const textoBuscar =
            txtBuscarEmpresa.value
                .toLowerCase()
                .trim();


        const filas =
            cuerpoEmpresas.querySelectorAll("tr");


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


 
    // ==========================================
    // ACTUALIZAR CANTIDAD
    // ==========================================
    function actualizarCantidadEmpresas() {

        lblCantidadEmpresas.textContent = cuerpoEmpresas.rows.length;
    }

});