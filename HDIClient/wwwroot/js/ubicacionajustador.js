
// Variables
let mapa = null;	// Mapa de Google Maps
// Iniciamos con la ubicación de la FEI
let latitud = 19.541142;
let longitud = -96.9271873;
// Coordenadas de donde esta el cliente
let latitudHome;
let longitudHome;
//let transportesSelect = document.getElementById("Transporte");
//let rutaCheck = document.querySelector("#Ruta");
let directionsRenderer = new google.maps.DirectionsRenderer();

// Esta función dibuja el mapa y coloca un marcador seleccionable en la FEI
function dibujaMapa() {
    console.log("dentro de funcion: " + latitud + " y " + longitud)
    
    mapa = $('#mapa').locationpicker({
        location: { latitude: latitud, longitude: longitud },
        radius: 300,
        addressFormat: 'point_of_interest',
        inputBinding: {
            locationNameInput: $('#Localizador')
        },
        enableAutocomplete: true,
        enableReverseGeocode: true,
        onchanged: function (currentLocation, radius, isMarkerDropped) {
            latitud = currentLocation.latitude;
            longitud = currentLocation.longitude;
            distancia();
        }
    });
}

function obtenerDireccionPalabras(latitud, longitud) {
    let geocoder = new google.maps.Geocoder();
    let latlng = new google.maps.LatLng(latitud, longitud);

    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                // Obtener la dirección en palabras
                let direccion = results[1].formatted_address;

                // Hacer algo con la dirección, por ejemplo, imprimir en la consola
                console.log('La dirección es:', direccion);
            }
        }
    });
}

function miUbicacion() {
    // Obtiene el mapa
    let mapContext = mapa.locationpicker('map');
    // Probamos el API HTML5 de geolocalización esta disponible en el cliente
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                latitudHome = position.coords.latitude;
                longitudHome = position.coords.longitude;
                new google.maps.Marker({
                    position: { lat: latitudHome, lng: longitudHome },
                    map: mapContext.map,
                    title: "Esta es tu ubicación actual",
                    icon: "images/home.png",
                   
                });
                distancia();
            },
            () => {
                $('#Distancia').val("La localización no está activada.");
            }
        );
    } else {
        $('#Distancia').val("El navegador no soporta geolocalización");
    }
}

function distancia() {
    // Obtiene el mapa
    let mapContext = mapa.locationpicker('map');
    console.log("dentro de funcion2: " + latitud + " y " + longitud)
    console.log("imprimiendo  la ubicacion en palabras")
    obtenerDireccionPalabras(latitud, longitud);
    // Inicia los servicios para la distancia
    const service = new google.maps.DistanceMatrixService();
    const selectedMode = 'DRIVING'

    // Petición para la distancia
    const origen = { lat: latitudHome, lng: longitudHome };
    const destino = { lat: latitud, lng: longitud };
    const request = {
        origins: [origen],
        destinations: [destino],
        travelMode: google.maps.TravelMode[selectedMode],
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false,
    };

    // Obtiene la distancia usando Google Matrix
    service.getDistanceMatrix(request).then((response) => {
        if (response.rows.length > 0) {
            $('#Distancia').val(response.rows[0].elements[0].distance.text);
            $('#Tiempo').val(response.rows[0].elements[0].duration.text);
        }
    });

    // Ahora vamos a dibujar la ruta
    //if (rutaCheck.checked) {
    //    const directionsService = new google.maps.DirectionsService();
    //    directionsRenderer.setMap(mapContext.map);

    //    directionsService
    //        .route({
    //            origin: origen,
    //            destination: destino,
    //            // Javascript nos permite acceder a la constante usando corchetes y un valor 
    //            // de cadena como la propiedad
    //            travelMode: google.maps.TravelMode[selectedMode],
    //        })
    //        .then((response) => {
    //            directionsRenderer.setDirections(response);
    //        });
    //} else {
    //    directionsRenderer.setMap(null);
    //}
}


// Calcula la distancia al cambiar de transporte
//[transportesSelect, rutaCheck].forEach(item => {
//    item.addEventListener("change", function () {
//        distancia();
//    });
//});

// Se inicia cuando la página ha cargado por completo

//GEOLOCALIZACION DE HTML5
function obtenerUbicacion() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            function (position) {
                // Accede a las coordenadas de la ubicación del usuario
                 latitud = position.coords.latitude;
                 longitud = position.coords.longitude;

                // Haz algo con las coordenadas (por ejemplo, muestra en la consola)
                console.log("Latitud: " + latitud + ", Longitud: " + longitud);
                dibujaMapa();
                miUbicacion();
            },
            function (error) {
                // Maneja posibles errores de geolocalización
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        console.error("El usuario denegó la solicitud de geolocalización.");
                        break;
                    case error.POSITION_UNAVAILABLE:
                        console.error("La información de la ubicación no está disponible.");
                        break;
                    case error.TIMEOUT:
                        console.error("La solicitud de geolocalización ha caducado.");
                        break;
                    case error.UNKNOWN_ERROR:
                        console.error("Se produjo un error desconocido al obtener la ubicación.");
                        break;
                }
            }
        );
    } else {
        console.error("Geolocalización no es compatible con este navegador.");
    }
}

// Llama a la función para obtener la ubicación del usuario

$(function () {
    console.log("ASDKJADSADS");
    obtenerUbicacion();
    
});