// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    //your code here

    $("#btnSubmit").click(function () {

        var cityname = $("#txtCity").val();
        if (cityname.length > 0) {
            $.ajax({
                url: "https://localhost:44393/Weather/WeatherDetail?city=" + cityname,
                type: "POST",
                success: function (rsltval) {
                    var data = JSON.parse(rsltval);
                    console.log(data);
                    $("#lblCity").html(data.City);
                    $("#lblCountry").text(data.Country);
                    $("#lblLat").text(data.Lat);
                    $("#lblLon").text(data.Lon);
                    $("#lblDescription").text(data.Description);
                    $("#lblHumidity").text(data.Humidity);
                    $("#lblTempFeelsLike").text(data.TempFeelsLike);
                    $("#lblTemp").text(data.Temp);
                    $("#lblTempMax").text(data.TempMax);
                    $("#lblTempMin").text(data.TempMin);
                    $("#imgWeatherIconUrl").attr("src", "http://openweathermap.org/img/w/" + data.WeatherIcon + ".png");
                    var a = "";
                    $.ajax({
                        url: "https://localhost:44393/Weather/GetCountryImage?x=32&&y=24&&countryCode=" + data.Country.toLowerCase(),
                        type: "POST",
                        success: function (link) {
                            a = link;
                            $("#imgCountry").attr("src",a);
                        },
                        error: {

                        }
                    });

                    $("#gmap_canvas").attr("src", "https://maps.google.com/maps?q=" + data.City+"&z=12&output=embed", )
                },
                error: function () {

                }
            });
        }
        else {
            alert("City Not Found");
        }
    });
});
