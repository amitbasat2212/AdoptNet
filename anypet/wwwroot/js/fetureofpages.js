document.getElementById("Adopt").disabled = true;

function GetMap() {
    var map = new Microsoft.Maps.Map('#myMap', {
        credentials: 'AgkL-WrWqfBFm2nN5EY0u4AMVcS8ezrnW6opuOOenpTkVizmr8FrlkRkfCJbsfD0',
        /* No need to set credentials if already passed in URL */
        center: new Microsoft.Maps.Location(31.9700919, 34.77205380048267),
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        zoom: 8
    });


    let location;
    const bing_key = 'AgkL-WrWqfBFm2nN5EY0u4AMVcS8ezrnW6opuOOenpTkVizmr8FrlkRkfCJbsfD0';
    var pin;
    var pin_location;

    $.ajax({
        url: 'https://' + new URL(window.location.host) + '/Associations/GetAssociationPlace',
        type: 'GET',
        success: function (data) {
            $.each(data, function (index) {
                setTimeout(() => {
                    location = data[index].location;

                    pin_location = getLatLon(location, bing_key);
                    console.log(location);

                    pin = new Microsoft.Maps.Pushpin(pin_location);
                    map.entities.push(pin);
                }, index * 200);
            });
           
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function getLatLon(query, bing_key) {
    var latlon;
    var mapObject;
    $.ajax({
        method: 'GET',
        url: `https://dev.virtualearth.net/REST/v1/Locations?q=${query}&key=${bing_key}`,
        async: false,
        success: function (data) {
            latlon = data.resourceSets[0].resources[0].point.coordinates;
            mapObject = new Microsoft.Maps.Location(latlon[0], latlon[1])
           
        },
        error: function (err) {
            console.log(err);
        }
    });
    return mapObject;
}

