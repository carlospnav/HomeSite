/*exported ShowLoading*/

//Shows the Loading gif.
function ShowLoading() {
    $('#top-loading-gif').css('visibility', 'visible');
}

//Hides the Loading gif.
function HideLoading() {
    $('#top-loading-gif').css('visibility', 'hidden');
}

//Adds the links that retrieve the detail informations to the main page.
function AddDetailsLinks() {
    var id = this.id;
    var descriptionPage = $("#side-page");
    var togglerVar = descriptionPage.attr("data-toggler");
    if (togglerVar === "off") {
        descriptionPage.attr("data-toggler", "on");
        descriptionPage.append("<div id='element-details-navigation-wrapper'><nav id='element-details-navigation-bar'></nav></div><div id='element-details-page-wrapper'>" +
             "<div id='element-details-description-container'><div id='element-details-image-container'><img id='element-details-image' src='/Content/Images/AboutMe/ContentDetails/default.jpg'>" +
             "</img></div><div id='element-details-text-container'><p id='element-details-title'></p><p id='element-details-text'>Click on one of the buttons above to display details about each main topic.</p></div></div></div>");
        descriptionPage.css("height", "18em");
        descriptionPage.css("transition", "height 1s ease-in-out");
        $.ajax({
            url: "/AboutMe/GetDetailsSpecificsInfo",
            data: { idParam: id },
            beforesend: ShowLoading(),
            success: function (data) {
                $("#element-details-navigation-bar").append("<ul></ul>");
                for (var counter = 0; counter < data.length; counter++) {
                    var idcounter = counter;
                    var nameid;
                    var detailDesc;
                    var detailName;
                    var detailImg;
                    var listItem = $("<li></li>");
                    listItem.attr("data-id", idcounter);
                    nameid = listItem.attr("data-id");
                    detailDesc = data[nameid].detailDesc;
                    detailName = data[nameid].detailName;
                    detailImg = data[nameid].detailImg;
                    listItem.click({ detailDesc: detailDesc, detailName: detailName, detailImgUrl: detailImg }, SetElementDetails);
                    $("#element-details-navigation-bar ul").append(listItem);
                }
                HideLoading();
            },
            error: function (xhr) {
                if (xhr.status === 404) {
                    $("#element-details-title").text("Details not found");
                    $("#element-details-text").text("The details for this profile could not be found. Please try again later or contact the administrator if the error persists.");
                }
                else {
                    $("#element-details-title").text("Server problem");
                    $("#element-details-text").text("There was a problem with your request. Please try again later.");
                }
                HideLoading();
            }
        });
        descriptionPage.on("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function () {
            $("#element-details-page-wrapper").css("opacity", "1");
            $("#element-details-navigation-wrapper").css("opacity", "1");
        });
    }
    else if (togglerVar === "on") {
        descriptionPage.off("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd");
        $("#element-details-navigation-wrapper").remove();
        $("#element-details-page-wrapper").remove();
        descriptionPage.css("height", "9.5em");
        descriptionPage.css("transition", "height 1s ease-in-out");
        descriptionPage.attr("data-toggler", "off");
    }
}

//Sets the text and title of each details of each Interest/Proficiency.
function SetElementDetails(event) {
    $("#element-details-text").text(event.data.detailDesc);
    $("#element-details-title").text(event.data.detailName);
    $('#element-details-image').attr('src', event.data.detailImgUrl);
}

//Sets the middle details of each Interest/Proficiency. 
function AddDetailsMouseOvers() {
    $('#side-page').css('visibility', 'visible');
    var id = this.id;
    $.ajax({
        url: "/AboutMe/GetDetailsMainSection",
        data: { idParam: id },
        beforesend: ShowLoading(),
        success: function (data) {
            var side = $("#side-page");
            side.replaceWith(data);
            HideLoading();
        },
        error: function (event) {
            if (event.status === 404) {
                console.log("The details for this section could not be found. Please alert the administrator to make sure the correct idParam is being sent.");
            }
            else {
                console.log("There was a problem with this request. Please try again later.");
            }
            HideLoading();
        }
    });
}

//Adds all the bindings in the page. Necessary to be a function as it's called by the ajax call in the cshtml.
function AddDetailsBindings() {
    HideLoading();
    $(".main-details-list-item").each(function () {
        $(this).mouseover(AddDetailsMouseOvers);
        $(this).click(AddDetailsLinks);
    });
}

(function () {
    $('#side-page').css('visibility', 'hidden');
    $(document).ready(function () {
        AddDetailsBindings();
    });
})();