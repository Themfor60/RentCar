function initAutocomplete() {
        const input = document.getElementById("ciudadCodigo");
    const autocomplete = new google.maps.places.Autocomplete(input, {
        types: ['(cities)'], 
    componentRestrictions: {country: "do" } 
        });

    autocomplete.addListener("place_changed", function () {
            const lugar = autocomplete.getPlace();
    console.log("Ciudad seleccionada:", lugar.formatted_address);
        });
    }

 google.maps.event.addDomListener(window, 'load', initAutocomplete);

