function AsignarAjustador(button) {
    var idAccident = button.getAttribute('data-id').toString();
    var location = button.getAttribute('data-location').toString();
    var accidentDate = button.getAttribute('data-date').toString();
    

    const url = `AssignLossAdjusterExtend?idAccident=${idAccident}&location=${location}&accidentDate=${accidentDate}`;

    window.location.href = url;
}