document.addEventListener('DOMContentLoaded', function () {
    var selectButtons = document.querySelectorAll('.select-vehicle');

    selectButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var vehicleId = this.getAttribute('data-vehicle-id');
            showVehicleInfo(vehicleId);
        });
    });

    function showVehicleInfo(vehicleId) {
        // Obtener la lista de vehículos desde el modelo
        var vehicleList = @Html.Raw(Json.Serialize(Model.policyList));
        console.log("ID del Vehículo select:", vehicleId);
        for (var i = 0; i < vehicleList.length; i++) {
            var vehiculo = vehicleList[i];

            // Comparar el vehicleId
            if (vehiculo.idVehicleClient === vehicleId) {
                // Has encontrado el vehículo deseado
                console.log("Vehículo encontrado:", vehiculo);

                // Acceder a las propiedades de vehicleClient
                var idVehicleClient = vehiculo.vehicleClient.idVehicleClient;
                var brand = vehiculo.vehicleClient.brand;
                var color = vehiculo.vehicleClient.color;
                var model = vehiculo.vehicleClient.model;
                var plate = vehiculo.vehicleClient.plate;
                var serialNumber = vehiculo.vehicleClient.serialNumber;
                var year = vehiculo.vehicleClient.year;

                var infoModel = document.getElementById('infoModel');
                var infoColor = document.getElementById('infoColor');
                var infoBrand = document.getElementById('infoBrand');
                var infoYear = document.getElementById('infoYear');
                var infoSerialNumber = document.getElementById('infoSerialNumber');
                var infoPlate = document.getElementById('infoPlate');

                // Actualizar el contenido de la sección de información del vehículo
                infoModel.innerText = model;
                infoColor.innerText = color;
                infoBrand.innerText = brand;
                infoYear.innerText = year;
                infoSerialNumber.innerText = serialNumber;
                infoPlate.innerText = plate;
                
                break;
            }
        }

    }
});