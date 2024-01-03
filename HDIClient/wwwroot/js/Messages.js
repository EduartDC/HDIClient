function verificarMensajesTempData() {
    if (TeampData.hasOwnProperty("Prueba") && TempData["Prueba"]) {
        mensajePrueba('Mensaje de prueba');
    }

    function mensajePrueba(mensaje) {
        Swal.fire({
            icon: "error",
            text: mensaje
        });
    }