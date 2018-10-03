var baseUrl = "http://localhost:53022/";
var timeOfDayUrl = "api/v1/movies/filters/timeOfDay";
var genreUrl = "api/v1/movies/filters/genre";
var currentDate = new Date().toISOString().slice(0, 10);
var movieUrl = "api/v1/movies?timeOfDayId=&genreId=&startIndex=0&endIndex=20&movieDate=" + currentDate; 
$(document).ready(function () {
    //init set filters values 
    $(function () {
        HttpGet(timeOfDayUrl, Load_timeOfDay);
        HttpGet(genreUrl, Load_Genre);
        HttpGet(movieUrl, Load_Movies);
    });

    $(function () {
        $.each([0,1,2,3,4,5], function (idx, item) {
            var date = new Date();
            date.setDate(date.getDate() + idx);
            var result = idx == 0 ? "Today" : date.toDateString();
            var dateHtml = "<button id='date" + idx + "' type='button' class='btn btn-default btn-margin'>" + result + "</button >";
            $("#date").append(dateHtml);
        });
    });

    $('#dateVal').val(currentDate);

    $("#date").on("click", "#date0, #date1, #date2, #date3, #date4, #date5", function () {
        var id = $(this).attr('id');
        setSearchDate(id.slice(4));  
        SearchMovie();
    });

    $("#search").click(function () {
        SearchMovie();
    });     
});

function setSearchDate(idx) {
    var date = new Date();
    date.setDate((date.getDate()) + parseInt(idx));
    currentDate = date.toISOString().slice(0, 10);
    $('#dateVal').val(currentDate);
}

function SearchMovie() {
    var timeSearch = $("#timeOfDay div label input");
    var timeSearchVal = "";
    $.each(timeSearch, function (idx, item) {        
        if (item.checked) {
            timeSearchVal += timeSearchVal == "" ? item.value : ',' + item.value;
        }        
    });

    var genreSearch = $("#genre div label input");
    var genreSearchVal = "";
    $.each(genreSearch, function (idx, item) {
        if (item.checked) {
            genreSearchVal += genreSearchVal == "" ? item.value : ',' + item.value;
        }        
    });
    var url = "api/v1/movies?timeOfDayId=" + timeSearchVal + "&genreId=" + genreSearchVal + "&startIndex=0&endIndex=20&movieDate=" + $('#dateVal').val();
    HttpGet(url, Load_Movies);
}

function Load_timeOfDay(lstTimeOfDay) {
    $.each(lstTimeOfDay, function (idx, item) {
        var timeOfdayHtml = "<div class='checkbox'><label><input type='checkbox' id='#t" + item.TimeOfDayId +"' value='" + item.TimeOfDayId + "'>" + item.FilterName + "</label></div>";
        $("#timeOfDay").append(timeOfdayHtml);
    });
}

function Load_Genre(lstGenre) {
    $.each(lstGenre, function (idx, item) {
        var genreHtml = "<div class='checkbox'><label><input type='checkbox' id='#g" + item.GenreId +"' value='" + item.GenreId + "'>" + item.GenreName + "</label></div>";
        $("#genre").append(genreHtml);
    });
}

function Load_Movies(lstMovie) {
    $("#movies").empty();
    $.each(lstMovie, function (idx, item) {
        var movieImgHtml = "<div class='row' id='#" + item.MovieId + "'><div class='col-lg-4'><div class='panel panel-default'><div><img src='" + item.ImageUrl +"' class='img-movie' /></div ></div > </div >";
        var movieTitleHtml = "<div class='col-lg-8'><div class='panel panel-primary'><div class='panel-heading'><span class='font-size16'>" + item.Title + "</span >";
        var reatingHtml = "<div class='pull-right'><div class='btn-group open'><button type='button' class='btn btn-danger btn-xs'>" + item.Rated + "</button ></div ></div ></div ><div class='panel-body'>";
        var timeHtml = "";
        $.each(item.Sessions, function (sidx, session) {
            var time = new Date(session.MovieTime);
            timeHtml += "<button type='button' class='btn btn-default btn-margin'>" + time.toLocaleTimeString() + "</button >";
        });
        var viewDtsHtml = "</div><a href='#'><div class='panel-footer'><span class='pull-left'>View Details</span><span class='pull-right'><i class='fa fa-arrow-circle-right'></i></span><div class='clearfix'></div></div></a></div></div></div>";
        var result = (movieImgHtml + movieTitleHtml + reatingHtml + timeHtml + viewDtsHtml);
        $("#movies").append(result);
    });
}

function HttpGet(url, callbackfn) {
    $.ajax({
        type: "GET",
        url: baseUrl + url,
        headers: {
            'Access-Control-Allow-Origin': '*'
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            callbackfn(response)
        },        
        error: function (response) {
            callbackfn(response)
        }
    });
}