let usuarioExiste = false;
let tipoCliente = "";


// =========================================================
// BUSCAR USUARIO AL SALIR DEL CORREO
// =========================================================

document.getElementById("email").addEventListener("blur", function () {

    const valorEmail = document.getElementById("email").value;
    const valorIdEmpresa = document.getElementById("idEmpresa").value;

    if (valorEmail === "") {
        return;
    }

    const usuario = {
        email: valorEmail,
        idEmpresa: valorIdEmpresa
    };

    fetch("/Inicio/BuscarUsuario", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(usuario)
    })
        .then(response => response.json())
        .then(data => {

            const nombre = document.getElementById("nombre");
            const grupoNombre = document.getElementById("grupoNombre");
            const grupoClave = document.getElementById("grupoClave");

            document.getElementById("grupoSolicitudMayorista");

            const grupoDatosMayorista =
                document.getElementById("grupoDatosMayorista");

            const chkMayorista =
                document.getElementById("chkMayorista");


            if (data.existe) {

                usuarioExiste = true;
                tipoCliente = data.tipoCliente;

                grupoNombre.style.display = "block";

                nombre.value = data.nombre;
                nombre.disabled = true;

                if (tipoCliente === "Mayorista") {

                    grupoClave.style.display = "block";

                    grupoSolicitudMayorista.style.display = "none";
                    grupoDatosMayorista.style.display = "none";
                }
                else if (tipoCliente === "Solicitante") {

                    grupoClave.style.display = "none";

                    grupoSolicitudMayorista.style.display = "none";
                    grupoDatosMayorista.style.display = "none";

                    alert(
                        "Estimado " + data.nombre + ":\n\n" +
                        "Su solicitud para ser cliente mayorista se encuentra en proceso de aprobación.\n" +
                        "Una vez aprobada, le enviaremos por correo electrónico su clave temporal de acceso."
                    );

                }
                else {

                    grupoClave.style.display = "none";

                    grupoSolicitudMayorista.style.display = "block";
                    grupoDatosMayorista.style.display = "none";

                    chkMayorista.checked = false;
                }
            }
            else {

                usuarioExiste = false;
                tipoCliente = "";

                grupoNombre.style.display = "block";
                grupoClave.style.display = "none";

                grupoSolicitudMayorista.style.display = "block";
                grupoDatosMayorista.style.display = "none";

                chkMayorista.checked = false;

                nombre.value = "";
                nombre.disabled = false;
                nombre.focus();
            }

        });
});

document.getElementById("chkMayorista").addEventListener("change", function () {

    const grupoDatosMayorista =
        document.getElementById("grupoDatosMayorista");

    if (this.checked) {
        grupoDatosMayorista.style.display = "block";
    }
    else {
        grupoDatosMayorista.style.display = "none";
    }

});

// =========================================================
// BOTÓN CONTINUAR
// =========================================================

document.getElementById("btnContinuar").addEventListener("click", function () {

    const valorNombre = document.getElementById("nombre").value;
    const valorEmail = document.getElementById("email").value;
    const valorIdEmpresa = document.getElementById("idEmpresa").value;

    const quiereMayorista = document.getElementById("chkMayorista").checked;

    if (quiereMayorista) {

        const valorRut = document.getElementById("rut").value;
        const valorTelefono = document.getElementById("telefono").value;
        const valorDireccion = document.getElementById("direccion").value;

        if (valorNombre === "" ||
            valorRut === "" ||
            valorTelefono === "" ||
            valorDireccion === "") {

            alert("Complete todos los datos para solicitar ser cliente mayorista.");
            return;
        }
    }

    // -----------------------------------------------------
    // USUARIO NUEVO
    // -----------------------------------------------------

    if (!usuarioExiste) {

        if (valorNombre === "" || valorEmail === "") {
            alert("Ingrese nombre y correo.");
            return;
        }

        const usuario = {
            nombre: valorNombre,
            email: valorEmail,
            idEmpresa: valorIdEmpresa
        };

        if (quiereMayorista) {

            usuario.rut = document.getElementById("rut").value;
            usuario.telefono = document.getElementById("telefono").value;
            usuario.direccion = document.getElementById("direccion").value;
            usuario.tipoCliente = "Solicitante";
        }

        fetch("/Inicio/CrearUsuario", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)
        })
            .then(response => response.json())
            .then(data => {

                if (data.creado) {

                    if (quiereMayorista) {
                        alert("Su solicitud fue recibida, cuando este aprobada le enviaremos su clave temporal de acceso.");  
                    }
                    
                    window.location.href = data.url;
                    // usuarioExiste = true;
                    // tipoCliente = "Comun";
                }

            });

        return;
    }


    // -----------------------------------------------------
    // CLIENTE MAYORISTA
    // -----------------------------------------------------

    if (tipoCliente === "Mayorista") {

        const valorClave = document.getElementById("clave").value;

        if (valorClave === "") {
            alert("Ingrese su clave.");
            return;
        }

        const usuario = {
            email: valorEmail,
            idEmpresa: valorIdEmpresa,
            passwordHash: valorClave
        };

        fetch("/Inicio/ValidarClave", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)
        })
            .then(response => response.json())
            .then(data => {

                if (data.valido) {
                 
                    window.location.href = data.url;
                }
                else {
                    alert("Clave incorrecta.");
                }

            });

        return;
    }


    // -----------------------------------------------------
    // USUARIO COMÚN EXISTENTE QUE QUIERE SER MAYORISTA
    // -----------------------------------------------------

    if (usuarioExiste &&
        tipoCliente === "Comun" &&
        quiereMayorista) {

        const usuario = {
            nombre: valorNombre,
            email: valorEmail,
            idEmpresa: valorIdEmpresa,
            rut: document.getElementById("rut").value,
            telefono: document.getElementById("telefono").value,
            direccion: document.getElementById("direccion").value,
            tipoCliente: "Solicitante"
        };

        fetch("/Inicio/CrearUsuario", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)
        })
            .then(response => response.json())
            .then(data => {

                if (data.creado) {

                    alert(
                        "Estimado " + valorNombre +
                        ", su solicitud fue creada correctamente. " +
                        "Una vez aprobada, le entregaremos su clave de acceso " +
                        "para que pueda comprar y disfrutar de las ofertas."
                    );

                    window.location.href = data.url;
                }

            });

        return;
    }


    // -----------------------------------------------------
    // USUARIO COMÚN EXISTENTE
    // -----------------------------------------------------

    if (tipoCliente === "Comun" ||
        tipoCliente === "Solicitante") {

        window.location.href =
            "/Catalogo/Index?idEmpresa=" + valorIdEmpresa;

        return;
    }


});