function updOpinion() {
    var description = document.getElementById('opinionTextArea').value;
    var idOpinionAdjuster = document.getElementById('idOpinionAdjuster').value;
    
    
    // Validate the description before sending it to the server
    if (validateDescription(description)) {
        
        var dataToSend = {
            CreationDate: null,
            Description: description,
            IdAccident: null,
            IdOpinionAdjuster: idOpinionAdjuster
        };
        // Send the description to the server via Ajax
        $.ajax({
            url: '/UpdateOpinion',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataToSend),
            success: function (response) {
                // Handle the server response
                console.log('Server response:', response);
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Se actualizó la información de forma correcta.',
                    onClose: function () {
                        // Redirect to another page or perform other actions after closing the message
                        window.location.href = '/ShowReportsAdjuster';
                    }
                });
            },
            error: function (xhr, textStatus, errorThrown) {
                // Handle errors in the Ajax request
                console.error('Error sending data to the server:', xhr.responseText);
                try {
                    var errorResponse = JSON.parse(xhr.responseText);
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: errorResponse.ErrorMessage || 'Hubo un error al procesar la solicitud.',
                    });
                } catch (error) {
                    // Si no se puede analizar el JSON, muestra un mensaje genérico
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Hubo un error al procesar la solicitud. Inténtelo nuevamente.',
                    });
                }
            }
        });
    }
}

function newOpinion() {
    var description = document.getElementById('opinionTextArea').value;
    var idReport = document.getElementById('idReport').value;
    // Crear un objeto con todos los datos que deseas enviar
    
    if (validateDescription(description)) {
        var dataToSend = {
            Description: description,
            IdAccident: idReport
        };
        $.ajax({
            url: '/CreatOpinion',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataToSend),
            success: function (response) {
                // Handle the server response
                console.log('Server response:', response);
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Se guardo la información de forma correcta.',
                    onClose: function () {
                        // Redirect to another page or perform other actions after closing the message
                        window.location.href = '/ShowReportsAdjuster';
                    }
                });
            },
            error: function (xhr, textStatus, errorThrown) {
                // Handle errors in the Ajax request
                console.error('Error sending data to the server:', xhr.responseText);
                try {
                    var errorResponse = JSON.parse(xhr.responseText);
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: errorResponse.ErrorMessage || 'Hubo un error al procesar la solicitud.',
                    });
                } catch (error) {
                    // Si no se puede analizar el JSON, muestra un mensaje genérico
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Hubo un error al procesar la solicitud. Inténtelo nuevamente.',
                    });
                }
            }
        });
    }
}

function validateDescription(description) {
    // Logic to validate the description
    if (description.trim() === '' || !/^[a-zA-Z0-9áéíóúüÁÉÍÓÚÜ.,;-\s]*$/.test(description)) {
        // Show SweetAlert2 in case of an error
        Swal.fire({
            icon: 'error',
            title: 'Error de validación',
            text: 'La descripción no puede estar vacía y no puede contener caracteres especiales.'
        });
        return false;
    }

    return true;
}

function habilitarEdicion() {
    // Habilita la edición del textarea
    var textarea = document.getElementsByName("description")[0];
    textarea.readOnly = false;

    // Oculta el botón de editar
    var btnEditar = textarea.nextElementSibling;
    btnEditar.style.display = "none";

    // Muestra el botón de guardar
    var btnGuardar = document.getElementById("btnGuardar");
    btnGuardar.style.display = "block";
}
function limpiarTextarea() {
    var textarea = document.querySelector('textarea[name="description"]');
    textarea.value = ''; // Limpiar el contenido del textarea
    textarea.removeAttribute('readonly');
    // Oculta el botón de editar
    var btnEditar = textarea.nextElementSibling;
    btnEditar.style.display = "none";
    document.getElementById('btnGuardar').style.display = 'block';
}