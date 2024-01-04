
var involvedList = []; // Lista para almacenar los involucrados
// Lista para almacenar los arreglos de bytes de las imágenes
var imageByteList = [];

//funcion para mostrar la informacion del vehiculo
document.getElementById('showVehicleInfo').addEventListener('change', function () {
    var vehicleInfoDiv = document.getElementById('vehicleInfo');
    vehicleInfoDiv.style.display = this.checked ? 'block' : 'none';
    if (this.checked) {
        involucradoCard.css('height', '600px');
    } else {
        involucradoCard.css('height', '350px');
    }
});

$(document).ready(function () {
    var editingIndex = -1;
    var isEditing = false;
    // Función para mostrar la lista de involucrados
    function showInvolvedList() {
        var tableBody = $('#involvedList'); // Obtén el cuerpo de la tabla

        tableBody.empty(); // Limpia el contenido actual del cuerpo de la tabla

        involvedList.forEach(function (involved, index) {
            var tableRow = $('<tr></tr>');
            tableRow.append('<td>' + involved.involvedName + '</td>');
            tableRow.append('<td>' + involved.involvedLastName + '</td>');
            tableRow.append('<td>' + involved.involvedNumber + '</td>');

            var actionsCell = $('<td class="text-center"></td>'); // Agrega la clase text-center para centrar el contenido

            // Botón de editar con ícono
            var editButton = $('<button class="btn btn-warning btn-sm icon-btn" id="editbtn"><i class="fas fa-edit"></i></button>');
            editButton.click(function () {
                editInvolved(index);
            });

            // Botón de eliminar con ícono
            var deleteButton = $('<button class="btn btn-danger btn-sm icon-btn" id="deletebtn"><i class="fas fa-trash-alt"></i></button>');
            deleteButton.click(function () {
                deleteInvolved(index);
            });

            actionsCell.append(editButton);
            actionsCell.append(deleteButton);

            tableRow.append(actionsCell);
            tableBody.append(tableRow);
        });
    }

    // Función para agregar un nuevo involucrado
    window.newInvolved = function () {
        $('#alertnull').remove();
        $('#alertname').remove();
        $('#alertnum').remove();
        var name = $('#involvedNameid').val();
        var lastName = $('#involvedLastNameid').val();
        var licens = $('#involvedNumberid').val();

        if (name.trim() === '' || lastName.trim() === '' || licens.trim() === '') {
            
            var errorMessage = $('<div class="alert alert-danger" role="alert" id="alertnull">Nombre, Apellido y No. Licencia son obligatorios.</div>');
            $('#formInvolved').prepend(errorMessage);
            return;
        }
        // Validación de caracteres permitidos en el nombre y apellido
        if (!/^[a-zA-Z\s]+$/.test(name) || !/^[a-zA-Z\s]+$/.test(lastName)) {
            var errorMessage = $('<div class="alert alert-warning" role="alert" id="alertname">Nombre y Apellido solo deben contener letras y espacios.</div>');
            $('#formInvolved').prepend(errorMessage);
            return;
        }

        // Validación de caracteres permitidos en el número de licencia
        if (!/^[\w\s-]+$/.test(licens)) {
            var errorMessage = $('<div class="alert alert-warning" role="alert" id="alertnum">Número de Licencia solo debe contener letras, números, espacios y el carácter "-".</div>');
            $('#formInvolved').prepend(errorMessage);
            return;
        }
        // Copia el involucrado antiguo antes de editarlo
        involvedToRemove = involvedList[editingIndex];

        if (editingIndex !== -1) {
            // Si estamos editando, actualizamos el involucrado existente en lugar de agregar uno nuevo
            involvedList[editingIndex] = {
                involvedName: name,
                involvedLastName: lastName,
                involvedNumber: licens,
                brand: $('#brandid').val(),
                model: $('#modelid').val(),
                plate: $('#plateid').val(),
                color: $('#colorid').val()
            };

            // Reiniciamos el índice de edición y la variable de edición
            editingIndex = -1;
            isEditing = false;

            // Cambia el texto del botón a "Nuevo Registro" después de la edición
            $('#newInvolvedBtn').text('Nuevo Registro');
        } else {
            // Si no estamos editando, agregamos un nuevo involucrado
            var newInvolved = {
                involvedName: name,
                involvedLastName: lastName,
                involvedNumber: licens,
                brand: $('#brandid').val(),
                model: $('#modelid').val(),
                plate: $('#plateid').val(),
                color: $('#colorid').val()
            };
            involvedList.push(newInvolved);
        }

        // Llama a la función para eliminar el involucrado antiguo después de editar
        removeOldInvolved();

        showInvolvedList();
        clearForm();
    };

    // Función para editar un involucrado
    function editInvolved(index) {       
        var involved = involvedList[index];
        $('#involvedNameid').val(involved.involvedName);
        $('#involvedLastNameid').val(involved.involvedLastName);
        $('#involvedNumberid').val(involved.involvedNumber);
        $('#brandid').val(involved.brand);
        $('#modelid').val(involved.model);
        $('#plateid').val(involved.plate);
        $('#colorid').val(involved.color);

        $('#newInvolvedBtn').text('Guardar Cambios');
        // Establece el índice de edición
        editingIndex = index;
        isEditing = true;      
    }
    var involvedToRemove;

    // Función para eliminar el involucrado antiguo después de editar
    function removeOldInvolved() {
        if (editingIndex !== -1 && involvedToRemove) {
            involvedList.splice(editingIndex, 1);
            showInvolvedList();
            involvedToRemove = undefined; // Restablece la variable
        }
    }

    // Función para eliminar un involucrado
    function deleteInvolved(index) {
        involvedList.splice(index, 1);
        showInvolvedList();
    }

    // Función para limpiar el formulario después de agregar un involucrado
    function clearForm() {
        $('#involvedNameid').val('');
        $('#involvedNumberid').val('');
        $('#involvedLastNameid').val('');
        $('#brandid').val('');
        $('#modelid').val('');
        $('#plateid').val('');
        $('#colorid').val('');
    }
});

let camara_boton = document.querySelector("#iniciar-camara");
let video = document.querySelector("#video");
let clic_boton = document.querySelector("#clic-foto");
let canvas = document.querySelector("#canvas");
let dataurl = document.querySelector("#dataurl");
let dataurl_container = document.querySelector("#dataurl-container");

camara_boton.addEventListener('click', async function () {
    let stream = null;

    try {
        stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
    }
    catch (error) {
        alert(error.message);
        return;
    }

    video.srcObject = stream;

    video.style.display = 'block';
    camara_boton.style.display = 'none';
    clic_boton.style.display = 'block';
});


clic_boton.addEventListener('click', function () {
    if (imageByteList.length < 8) {
        canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
        let image_data_url = canvas.toDataURL('image/jpeg');

        
        // Agregar el arreglo de bytes a la lista
        imageByteList.push(image_data_url);
        console.log('Cantidad de arreglos en la lista:', imageByteList.length);

        // Obtener la fecha y hora actual
        var currentDate = new Date();
        var formattedDate = currentDate.toISOString().replace(/[^a-zA-Z0-9]/g, '');

        // Crear un nuevo elemento li
        var newLi = document.createElement('li');
        newLi.id = "liimageid";

        const p = document.createElement("p");
        p.textContent = formattedDate;
        newLi.appendChild(p);

        // Agregar el nuevo elemento li al ul
        var ul = document.querySelector('#dataurl-container ul');
        newLi.appendChild(addDeleteBtn());
        ul.appendChild(newLi);
    } else {
        console.log('Se ha alcanzado el límite máximo de 8 imágenes.');
    }
});


function addDeleteBtn() {
    const deleteBtn = document.createElement("button");
    deleteBtn.textContent = "X";
    deleteBtn.className = "btn-delete";

    deleteBtn.addEventListener("click", (e) => {
        const item = e.target.parentElement;
        var ul = document.querySelector('#dataurl-container ul');
        const index = Array.from(ul.children).indexOf(item);

        ul.removeChild(item);
        imageByteList.splice(index, 1);
        console.log('Cantidad de arreglos en la lista:', imageByteList.length);
    });

    return deleteBtn;
}
function sendInfo() {

    let involvedData = involvedList; // La lista de involucrados
    let imageByteListData = imageByteList; // La lista de imágenes
    console.log('Cantidad de arreglos en la lista:', imageByteList.length);
    console.log('Cantidad de arreglos en la lista dos:', involvedData);
    console.log('Involved List:', involvedList);
    console.log('Image Byte List:', imageByteList);

    let latitud = document.querySelector('[name="Latitud"]').value;
    let longitud = document.querySelector('[name="Longitud"]').value;
    let localizador = document.querySelector('[name="Localizador"]').value;
    let idvehiculo = document.getElementById('infoId').value;
    // Validar los datos
    if (!validateData(involvedData, imageByteListData, latitud, longitud, localizador, idvehiculo)) {
        alert('Por favor, complete todos los campos y asegúrese de tener al menos un involucrado y cuatro imagenes.');
        return;
    }
    //objeto con todos los datos
    var dataToSend = {
        InvolvedDataList: involvedData,
        ImageByteList: imageByteListData,
        latitude: latitud,
        longitude: longitud,
        address: localizador,
        idcar: idvehiculo
    };

    $.ajax({
        url: '/InfoReport',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(dataToSend),
        success: function (response) {
            console.log('Respuesta del controlador:', response);

            Swal.fire({
                icon: 'success',
                title: 'Éxito',
                text: 'Datos recibidos y procesados exitosamente',
            }).then((result) => {
                // Redirigir a otra página después de hacer clic en "Aceptar" en SweetAlert2
                if (result.isConfirmed) {
                    window.location.href = '/Report/Tips'; 
                }
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error('Error al enviar datos al controlador:', xhr.responseText);

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
// Función de validación
function validateData(involvedData, imageByteListData, latitud, longitud, localizador, idvehiculo) {

    if (!involvedData || involvedData.length === 0) {
        console.error('La lista de involucrados está vacía o es nula.');
        return false;
    }

    if (!imageByteListData || imageByteListData.length === 0) {
        console.error('La lista de imágenes está vacía o es nula.');
        return false;
    }
    if (!imageByteListData || imageByteListData.length < 4) {
        console.error('Es necesario tener minimo 4 imagenes.');
        return false;
    }
    if (!latitud || !longitud || !localizador || !idvehiculo) {
        console.error('Alguno de los campos obligatorios está vacío.');
        return false;
    }
    // Si todas las validaciones pasan, retorna true
    return true;
}



