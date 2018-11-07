$("#button-all-data").click(function () {
    $.getJSON('https://beavers-directory.azurewebsites.net/api/values/', function (apiData) {
        console.log(apiData);
        var userData = JSON.stringify(apiData, null, 4);
        $("#api-data").empty().text(userData);
        $("#api-data").show();
    });
});
