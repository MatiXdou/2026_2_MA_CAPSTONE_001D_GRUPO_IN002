document.addEventListener("DOMContentLoaded", function () {

    const btnSidebar = document.getElementById("btnSidebar");

    if (!btnSidebar) {
        return;
    }

    btnSidebar.addEventListener("click", function () {

        if (window.innerWidth <= 991) {
            document.body.classList.toggle("sidebar-open");
        } else {
            document.body.classList.toggle("sidebar-hidden");
        }

    });

});