document.getElementById("txtBuscarProducto")
    .addEventListener("keyup", function () {

        const texto = this.value.toLowerCase();

        const productos =
            document.querySelectorAll(".producto-item");

        productos.forEach(function (producto) {

            const contenido =
                producto.innerText.toLowerCase();

            if (contenido.includes(texto)) {
                producto.style.display = "";
            }
            else {
                producto.style.display = "none";
            }

        });

    });