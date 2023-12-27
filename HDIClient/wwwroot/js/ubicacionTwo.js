// Variables
let mapa = null;	// Mapa de Google Maps
// Iniciamos con la ubicación de la FEI
let latitud = document.getElementById('latitudid').value;;
let longitud = document.getElementById('longitudid').value;;
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
            locationNameInput: $('#Localizador'),
        },
        enableAutocomplete: true,
        enableReverseGeocode: true,
        draggable: false,
    });

}


$(function () {   
    dibujaMapa();
});