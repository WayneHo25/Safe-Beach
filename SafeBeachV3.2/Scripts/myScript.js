/*
    Function: This is the code for dropdown menu of search choice. 

    Input: The choice of search option: suburb name, beach name and postcode.

    Output: None; change the value of dropdown menu and the value of hidden input which can use in Home Controller
    
    Notes: 1. The default value for hidden inout is suburb name. 
*/
$(document).ready(function (e) {
    $('.search-panel .dropdown-menu').find('a').click(function (e) {
        e.preventDefault();
        var param = $(this).attr("href").replace("#", "");
        var concept = $(this).text();
        $('.search-panel span#search_concept').text(concept);
        $('.input-group #search_param').val(param);
    });

    $(".searchButton").parent().parent().prop("data-toggle", "tooltip").prop("data-placement", "left").prop("title", "Search Beaches");
    $(".flagButton").parent().parent().prop("data-toggle", "tooltip").prop("data-placement", "left").prop("title", "Safety Flag Info");

    $('[data-toggle="tooltip"]').tooltip();
});

/*
    Function: This is the code for detecting the location of users. 

    Input: None; Gain the admission of getting current location from users.

    Output: The suburb name of users' current location.
    
    Notes: 1. This code should run in https://.
           2. The api key is included in the code. Dealing with the privacy problem later.
           3. Using results[0].address_components.length to check the length of result.
*/
function getLocation() {
    var x = document.getElementById("SearchString");
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(showPosition);
    } else {
        x.value = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    var x = document.getElementById("SearchString");
    var latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
    // This is making the Geocode request
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        'latLng': latlng
    }, function (results, status) {
        if (status !== google.maps.GeocoderStatus.OK) {
            alert(status);
        }
        // This is checking to see if the Geoeode Status is OK before proceeding
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                count = results[0].address_components.length;
                suburb = results[0].address_components[count-5].long_name;
                x.value = suburb;
            }
            else {
                x.value = "address not found";
            }
        }
    });
}