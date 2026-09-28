// =========================================================
// BOTÓN INGRESAR
// =========================================================

document.getElementById("btnIngresar").addEventListener("click", function () {

    const valorEmail = document.getElementById("email").value;
    const valorClave = document.getElementById("clave").value;

    if (valorEmail === "" || valorClave === "") {
        alert("Ingrese correo y clave.");
        return;
    }

    const usuario = {
        email: valorEmail,
        passwordHash: valorClave
    };

    fetch("/Administracion/Ingresar", {
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
                alert("Correo o clave incorrectos.");
            }

        });

});