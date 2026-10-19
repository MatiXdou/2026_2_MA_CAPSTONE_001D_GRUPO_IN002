
document.getElementById("formCambiarClave").addEventListener("submit", function (e) {

    e.preventDefault();

    const claveActual = document.getElementById("claveActual").value;
    const claveNueva = document.getElementById("claveNueva").value;
    const confirmarClave = document.getElementById("confirmarClave").value;

    if (!claveActual || !claveNueva || !confirmarClave) {
        alert("Debe ingresar toda la información solicitada.");
        return;
    }

    if (claveNueva !== confirmarClave) {
        alert("Las claves nuevas no coinciden.");
        return;
    }

    if (claveActual === claveNueva) {
        alert("La clave nueva debe ser diferente a la actual.");
        return;
    }

    if (!confirm("¿Está seguro de que desea cambiar su clave?")) {
        return;
    }

    const datos = new FormData();

    datos.append("claveActual", claveActual);
    datos.append("claveNueva", claveNueva);
    datos.append("confirmarClave", confirmarClave);

    fetch("/Cuenta/CambiarClave", {
        method: "POST",
        body: datos
    })
        .then(response => response.json())
        .then(data => {

            alert(data.mensaje);

            if (data.correcto) {
                document.getElementById("formCambiarClave").reset();
            }

        })
        .catch(() => {
            alert("Error al cambiar la clave.");
        });

});


function mostrarClave(id) {

    const campo = document.getElementById(id);

    campo.type = campo.type === "password" ? "text" : "password";

}

