// Variables
let mapa = null;	// Mapa de Google Maps
// Iniciamos con la ubicación de la FEI
let latitud = 19.541142;
let longitud = -96.9271873;
// Coordenadas de donde esta el cliente
let latitudHome;
let longitudHome;
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
                
            },
            () => {
                $('#Distancia').val("La localización no está activada.");
            }
        );
    } else {
        $('#Distancia').val("El navegador no soporta geolocalización");
    }
}



$(function () {
    
    dibujaMapa();
    miUbicacion();
    
});