$(document).ready(function () {
    $(".assign-button").on("click", function () {
        var idEmployee = $(this).data("id");
        var idAccident = document.getElementById("labelAccident").value;

        console.log("Antes de la solicitud");

        $.ajax({
            type: "POST",
            url: '/SaveAssignmentInDB',
            data: { idEmployee: idEmployee, idAccident: idAccident },
            success: function (response) {
                console.log('Respuesta del controlador:', response);
                // Puedes manejar la respuesta del controlador aquí
                Swal.fire({
                    icon: 'success',
                    title: 'Éxito',
                    text: 'Ajustador asignado exitosamente',
                }).then((result) => {
                    // Redirigir a otra página después de hacer clic en "Aceptar" en SweetAlert2
                    if (result.isConfirmed) {
                        window.location.href = '/Sinister/AssignLossAdjuster';
                    }
                });
            },
            error: function (error) {
                console.error("Error en la solicitud", error);
            }
        });
    });
});