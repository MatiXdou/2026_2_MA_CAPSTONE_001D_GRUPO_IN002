document.querySelectorAll(".btnAprobar").forEach(function (boton) {

    boton.addEventListener("click", function () {

        const confirmar = confirm(
            "¿Está seguro que desea aprobar esta solicitud mayorista?"
        );

        if (!confirmar) {
            return;
        }

        const idUsuario = this.dataset.id;

        const usuario = {
            idUsuario: idUsuario
        };

        fetch("/SolicitudMayorista/Aprobar", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)
        })
            .then(response => response.json())
            .then(data => {

                if (data.aprobado) {

                    alert("Solicitud aprobada correctamente.");

                    location.reload();
                }

            });

    });

});