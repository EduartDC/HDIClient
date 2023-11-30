// Variables
let mapa = null;	// Mapa de Google Maps
// Iniciamos con la ubicación de la FEI
let latitud = 19.541142;
let longitud = -96.9271873;
// Coordenadas de donde esta el cliente
let latitudHome;
let longitudHome;
let transportesSelect = document.getElementById("Transporte");
let rutaCheck = document.querySelector("#Ruta");
let directionsRenderer = new google.maps.DirectionsRenderer();

// Esta función dibuja el mapa y coloca un marcador seleccionable en la FEI
function dibujaMapa() {
    mapa = $('#mapa').locationpicker({
        location: { latitude: latitud, longitude: longitud },
        radius: 300,
        addressFormat: 'point_of_interest',
        inputBinding: {
            latitudeInput: $('#Latitud'),
            longitudeInput: $('#Longitud'),
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

function miUbicacion() {

    let mapContext = mapa.locationpicker('map');
    mapContext.map.setZoom(14);

    mapContext.map.clearMarkers();
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                latitudHome = position.coords.latitude;
                longitudHome = position.coords.longitude;
                new google.maps.Marker({
                    position: { lat: latitudHome, lng: longitudHome },
                    map: mapContext.map,
                    title: "Esta es tu ubicación actual",
                    icon: "images/home.png"
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
    let mapContext = mapa.locationpicker('map');

    const service = new google.maps.DistanceMatrixService();
    const selectedMode = document.getElementById("Transporte").value;

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

    service.getDistanceMatrix(request).then((response) => {
        if (response.rows.length > 0) {
            $('#Distancia').val(response.rows[0].elements[0].distance.text);
            $('#Tiempo').val(response.rows[0].elements[0].duration.text);
        }
    });

    if (rutaCheck.checked) {
        const directionsService = new google.maps.DirectionsService();
        directionsRenderer.setMap(mapContext.map);

        directionsService
            .route({
                origin: origen,
                destination: destino,
                travelMode: google.maps.TravelMode[selectedMode],
            })
            .then((response) => {
                directionsRenderer.setDirections(response);
            });
    } else {
        directionsRenderer.setMap(null);
    }
}

/*[transportesSelect, rutaCheck].forEach(item => {
    item.addEventListener("change", function () {
        distancia();
    });
});*/

$(function () {
    
    dibujaMapa();
    miUbicacion();
    
});