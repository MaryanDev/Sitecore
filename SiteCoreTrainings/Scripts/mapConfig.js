function initMap() {
    if (mapSettings !== undefined && mapSettings !== null) {
        var center = {
            lat: mapSettings.lat,
            lng: mapSettings.lng
        };
        var mapOptions = {
            center: center,
            zoom: 14,
            mapTypeId: google.maps.MapTypeId.HYBRID
        }
        var map = new google.maps.Map(document.getElementById("map"), mapOptions);
        var marker = new google.maps.Marker({
            position: center,
            map: map
        });
        google.maps.event.trigger(map, 'resize');
    }
}

$(document).ready(function () {
    $("#mapCollapse").attrchange({
        trackValues: true,
        callback: function (event) {
            if (event.attributeName === "aria-expanded")
                if (event.newValue === "true" || event.newValue === true) {
                    initMap();
                }
        }
    });
});