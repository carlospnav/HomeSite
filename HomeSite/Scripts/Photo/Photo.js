/*global isAuthed*/
/*exported generatePhotos, pageToUpdate */
var detailsPhotoId;
var pagination = 8;

jQuery.fn.showV = function () {
    this.css('visibility', 'visible');
}

jQuery.fn.hideV = function () {
    this.css('visibility', 'hidden');
}

//Binds the function that will like or dislike a photo to its button.
function addGetPhotoLikeCallback($element, photoId, orientation) {
    //determines if there are any 'click' events on the element passed as argument to this function. CHECK!
    var event = $._data($element, 'events');
    if (event !== undefined) {
        console.log('Evento bound: ' + event.click);
    }
    //Removes every click event from this element.
    $element.off('click');
    //Binds the "Like or Unlike a photo" click event to the element.
    $element.on('click', function () {
        LoaderShowHide();
        $.getJSON("../Photo/LikeOrUnlikeAPhoto", { photoId: photoId })
        .done(function () {
            $('.main-error-text').css('visibility', 'hidden');
            console.log("Like/Dislike adicionado com sucesso!");
            LoaderShowHide();
            updateLikeButton($element, photoId);
            changeLikeCount(orientation, photoId);
        })
        .fail(function (hxr) {
            LoaderShowHide();
            if (hxr.status === 404) {
                console.log('We were not able to find this photo. Please try again later or contact an admin.');
                $('.main-error-text').text('We were not able to find this photo. Please try again later or contact an admin.');
            }
            else {
                console.log('Failed to like or dislike the photo. Please try again later or contact an admin.');
                $('.main-error-text').text('Failed to like or dislike the photo. Please try again later or contact an admin.');
            }
            console.log('Error: ' + hxr.status);
            $('.main-error-text').css('visibility', 'visible');
        });
    });
}

//Checks for updates to the like button passed to the function and updates it to reflect whether the photo is liked or not.
function updateLikeButton($likeButton, photoId) {
    console.log('Checking to see if photo ' + photoId + ' is liked...');
    LoaderShowHide();
    $.getJSON("/Photo/GetPhotoLike", { photoId: photoId })
        .done(function (data) {
            LoaderShowHide();
            $('.main-error-text').css('visibility', 'hidden');
            if (data.vote === true) {
                $likeButton.attr('class', 'photo-like-container photo-like-activated');
                console.log('This photo is liked!');
            }
            else {
                $likeButton.attr('class', 'photo-like-container photo-like-initial');
                console.log('This photo is not liked!');
            }

        })
    .fail(function (xhr) {
        LoaderShowHide();
        if (xhr.status !== 200) {
            $('.main-error-text').text('There was a problem updating the button. Please try again later or contact an admin.');
            console.log("There was a problem updating the like button. Error: " + xhr.status);
            $('.main-error-text').css('visibility', 'visible');
        }
    });
}

//Creates and returns the button that sets and displays whether the photo is liked or not for each user.
function getPhotoLikeButton(photoId, orientation) {
    var photoLikeDiv = $('<div class="photo-like-container photo-like-initial"></div>');
    var likeThumb = $('<p>&#128077</p>');
    photoLikeDiv.append(likeThumb);
    addGetPhotoLikeCallback(photoLikeDiv, photoId, orientation); //
    updateLikeButton(photoLikeDiv, photoId); //
    return photoLikeDiv;
}

//Gets the total like count of a certain photo and updates the details page with it.
function changeLikeCount(orientation, photoId) {
    var likeCount;
    if (orientation === "Horizontal") {
        likeCount = $('#horizontal-like-count');
    }
    else {
        likeCount = $('#vertical-like-count');
    }
    console.log('Counting the total likes of photo ' + photoId);
    LoaderShowHide();
    $.getJSON("/Photo/GetLikeCount", { photoId: photoId })
    .done(function (data) {
        LoaderShowHide();
        if (orientation === "Vertical") {
            console.log('Updating photo ' + photoId + ' like count(vertical).');
            $('#vertical-details-error-text').hideV();
        }
        else {
            console.log('Updating photo ' + photoId + ' like count(horizontal).');
            $('#horizontal-details-error-text').hideV();
        }
        likeCount.text(data.likeCount + " likes");
    })
    .fail(function (xhr) {
        LoaderShowHide();
        likeCount.text('? likes');
        likeCount.addClass('like-count-error');
        if (orientation === "Horizontal") {
            $('#horizontal-details-error-text').text('There was a problem reading the like count for this photo. Please try again later or contact an admin.');
            $('#horizontal-details-error-text').showV();
        }
        else {
            $('#vertical-details-error-text').text('There was a problem reading the like count for this photo. Please try again later or contact an admin.');
            $('#vertical-details-error-text').showV();
        }
        console.log('Error retrieving the like count of the photo. Error: ' + xhr.status);
    });

}

//Hides or Shows the side loading icon animation.
function LoaderShowHide() {
    if ($('#top-loading-gif').css('visibility') !== 'hidden') {
        $('#top-loading-gif').hideV();
    }
    else
        $('#top-loading-gif').showV();
}

//Creates the error paragraph to be added to the page in case of an error.
function createErrorContainer(element, message, id) {
    var error = $('<p class="error-text"></p>');
    error.text(message);
    if (id !== undefined) {
        error.attr('id', id);
    }
    element.append(error);
}

//Creates the page links when the user clicks on an album.
function generatePhotos(albumId, pagination) {
    var pages;
    $.ajax({
        url: "/Photo/Paginator",
        data: { albumId: albumId, pagination: pagination },
        beforeSend: function () {
            LoaderShowHide();
        },
        success: function (data) {
            LoaderShowHide();
            pages = data.pages;
            var paginator = $("#paginator").empty(); //Get the container for the photo pages and clear it of previous album pages.

            for (var x = 0; x < pages; x++) {

                //Create the Pages and adds them to the Photo section.
                var pageLink = $('<a class="page-number"></a>');
                pageLink.text(x + 1);
                pageLink.attr("data-currPage", (x * pagination));
                pageLink.attr("data-endPage", ((x + 1) * pagination));
                pageLink.click({ pageLink: pageLink, albumId: albumId }, photosPageClick); //Callback that adds the page's photos to the Photos section.
                paginator.append(pageLink);
            }

            //Adding the Add-Photo button to the photo container.
            if ($("#photography-upper-wrapper").attr("data-auth") === "True") { //If user is authorized.
                if ($('#add-photo-button-container > a').length < 1) {
                    var buttonContainer = $("#add-photo-button-container");
                    var button = $('<a class="button" id="add-photo" href="/Photo/CreatePhoto?albumId=' + albumId + '"></a>');
                    var buttonText = $("<p>+</p>");
                    button.append(buttonText);
                    buttonContainer.prepend(button);
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            if (xhr.status === 404) {
                createErrorContainer($('#paginator'), "We could not find the photos of this album. Please contact the administrator.");
                console.log("Error: 404");
            }
            else {
                createErrorContainer($('#paginator'), "There was a problem with this request. Try again later or contact an admin.");
                console.log("Error: " + errorThrown);
            }
            LoaderShowHide();
        }
    });
}

//Retracts the horizontal textbox used to post comments into its shortened version when the user clicks outside of its container.
function retractHorizontalCommentsControls(event) {
    if (!$(event.target).closest('#horizontal-comment-expanded-container').length) {
        $('#horizontal-comment-expanded-container textarea').removeClass('horizontal-textarea-expanded');
        setTimeout(function () {
            $('#horizontal-comment-expandable-container').show();
            $('#horizontal-comment-expanded-container').hide();
        }, 1000);
        $('#horizontal-comment-expanded-container textarea').val('');
        $('#horizontal-comment-expanded-container label').css('color', 'white');
        $('#horizontal-comment-expanded-container label').text('');
        $(document).off('click', retractHorizontalCommentsControls);
    }
}

//Retracts the horizontal textbox used to post comments into its shortened version when the user clicks outside of its container.
function retractVerticalCommentsControls(event) {
    if (!$(event.target).closest('#vertical-comment-expanded-container').length) {
        $('#vertical-comment-expanded-container textarea').removeClass('vertical-textarea-expanded');
        setTimeout(function () {
            $('#vertical-comment-expandable-container').show();
            $('#vertical-comment-expanded-container').hide();
        }, 1000);
        $('#vertical-comment-expanded-container textarea').val('');
        $('#vertical-comment-expanded-container label').css('color', 'white');
        $('#vertical-comment-expanded-container label').text('');
        $(document).off('click', retractVerticalCommentsControls);
    }
}

//Adds the photo comments to the comments area when the user clicks on a comments page.
function getPaginatedComments(event) {
    var orientation = event.data.orientation;
    LoaderShowHide();
    $.getJSON("/Photo/GetPaginatedComments", { photoId: event.data.photoId, currentPagination: event.data.currentPage })
        .done(function (data) {
            LoaderShowHide();
            $('.comment-expandable-container').show();
            AddCommentsToDom(data, orientation);
        })
    .fail(function (xhr) {
        LoaderShowHide();
        var paginator;
        if (orientation === "Vertical") {
            paginator = $('#vertical-side-content').empty();
            createErrorContainer(paginator, 'There was an error finding the comments for this photo. Please try again later or contact an administrator.', 'main-comments-error-text');
            $('#vertical-comment-expandable-container').hide();
        }
        else {
            paginator = $('#horizontal-side-content').empty();
            createErrorContainer(paginator, 'There was an error finding the comments for this photo. Please try again later or contact an administrator.', 'main-comments-error-text');
            $('#horizontal-comment-expandable-container').hide();
        }
        console.log('We could not find the comments for this photo. Error: ' + xhr.status);
    });
}

//Generates the html for the individual comment and returns it.
function generateComment(userName, body) {
    var commentDiv = $('<div class="photo-comment"></div>');
    var titleDiv = $('<div class="comment-title"></div>');
    var labelName = $('<label class="photo-comment-user"></label>');
    labelName.html(userName);
    var labelDate = $('<label class="photo-comment-date"></label>');
    labelDate.html(Date.now());
    var commentBody = $('<div class="comment-body"></div>');
    var commentContent = $('<p></p>');
    commentContent.html(body);
    commentBody.append(commentContent);
    titleDiv.append(labelName);
    titleDiv.append(labelDate);
    commentDiv.append(titleDiv);
    commentDiv.append(commentBody);
    return commentDiv;
}

//Adds the commentary to the comments Page. 
function AddCommentsToDom(data, orientation) {
    var commentsPage;
    //Adds the comments to the proper orientation page.
    if (orientation === "Vertical") {
        commentsPage = $('#vertical-side-content').empty();
    }
    else if (orientation === "Horizontal") {
        commentsPage = $('#horizontal-side-content').empty();
    }
    else {
        console.log('There was a problem with the string that determines the orientation of the comments.');
    }
    if (data.length === undefined) {
        var noComments = $('<p>There are no comments yet for this photo. Be the first to make one!</p>');
        commentsPage.append(noComments);
    }
    if (data.length > 0) {
        for (var y = 0; y < data.length; y++) {
            //Add comment to page here.
            commentsPage.append(generateComment(data[y].userName, data[y].body));
        }
    }
}

//Updates the comments page to show the last comments on the last page. 
function getLastCommentPageToShow(orientation, photoId) {
    LoaderShowHide();
    $.getJSON("/Photo/GetLastComments", { photoId: photoId, commentsPerPage : 4 })
    .done(function (data) {
        LoaderShowHide();
        $('.comment-expandable-container').show();
        AddCommentsToDom(data, orientation);
    })
    .fail(function (xhr) {
        LoaderShowHide();
        var paginator;
        if (orientation === "Vertical") {
            paginator = $('#vertical-side-content').empty();
            createErrorContainer(paginator, 'There was an error finding the comments for this photo. Please try again later or contact an administrator.', 'main-comments-error-text');
            $('#vertical-comment-expandable-container').hide();
        }
        else {
            paginator = $('#horizontal-side-content').empty();
            createErrorContainer(paginator, 'There was an error finding the comments for this photo. Please try again later or contact an administrator.', 'main-comments-error-text');
            $('#horizontal-comment-expandable-container').hide();
        }
        console.log('We could not find the comments for this photo. Error: ' + xhr.status);
    });
}

//Generates the comment pages to be added to the details section of the photos. 
function generateCommentPages(photoId, orientation) {
    var pages;
    $.getJSON("/Photo/CommentsPaginator", { photoId: photoId })
        .done(function (data) {
            $('.comment-expandable-container').show();
            pages = data.pages; //Gets the number of pages for the comments.
            var paginator;
            if (orientation === "Vertical") {

                paginator = $('#vertical-comment-pages').empty();
            }
            else if (orientation === "Horizontal") {

                paginator = $('#horizontal-comment-pages').empty();
            }
            else {
                console.log("There was a problem with the string that determines the orientation of the comments pages.");
            }
            //Adds the comments page links with their click event bound to the comment page section.
            var x;
            for (x = 0; x < pages; x++) {

                var pageLink = $('<a class="page-number"></a>');
                pageLink.text(x + 1);
                pageLink.attr("data-currPage", (x * 4));
                pageLink.click({ currentPage: pageLink.attr('data-currPage'), photoId: photoId, orientation: orientation }, getPaginatedComments);
                paginator.append(pageLink);
            }
        })
    .fail(function (xhr) {
        var paginator;
        if (orientation === "Vertical") {
            paginator = $('#vertical-details-error-text');
            paginator.text('There was an error finding the comments for this photo. Please reload the website.');
            paginator.show();
        }
        else {
            paginator = $('#horizontal-details-error-text');
            paginator.text('There was an error finding the comments for this photo. Please reload the website.');
            paginator.show();
        }

        console.log('Could not find the number of comment pages to return. Error: ' + xhr.status);
    });
}

//Generates a comment tag and returns it.
function getNewCommentLabel(orientation) {
    var newComment = $('<label>New Comment!</label>');
    if (orientation === "Horizontal") {
        newComment.attr('id', 'horizontal-new-comment-label');
    }
    else if (orientation === "Vertical") {
        newComment.attr('id', 'vertical-new-comment-label');
    }
    else
        console.log("There was a problem with the orientation");

    return newComment;
}

//Gets the enlarged photo into the main area.
function getDetailedPhoto(event) {

    //sets variables to use later.
    var detailsContainer = $("#details-photo-container").empty();
    var thumbNail = event.data.thumbImage;
    var orientation = (thumbNail.attr("data-orientation"));
    console.log('O ID É: ' + thumbNail.attr('data-photoId'));
    $('#photography-upper-wrapper').attr('data-photoId', thumbNail.attr('data-photoId'));
    var photoId = thumbNail.attr("data-photoId");
    var photoUrl = thumbNail.attr("data-photoUrl");
    photoUrl = photoUrl.slice(1);

    var photoName = thumbNail.attr("data-photoName");
    var photoDesc = thumbNail.attr("data-photoDesc");
    var photoLike = thumbNail.attr("data-photoLike");



    var overviewcontainer;
    var overviewbackground;
    var imageId;
    var photoLikeDiv = getPhotoLikeButton(photoId, orientation); //gets the button to like a photo and stores in a variable.

    //Creates error container.
    var errorText = $('<p class="error-text main-error-text" >We were not able to find this photo. Please try again later or contact an admin.</p>');

    //creates the container that will hold the main photo. Either vertical or horizontal.
    if (orientation === "Vertical") {
        overviewcontainer = $('<div id="vertical-photo-container"></div>');
        var overviewBackgroundContainer = $('<div id="vertical-photo-background-container"></div>');
        overviewbackground = $('<div id="vertical-photo-background"></div>');
        overviewBackgroundContainer.append(overviewbackground);
        overviewcontainer.append(overviewBackgroundContainer);
        errorText.attr('id', 'vertical-main-photo-error-text');
    }
    else {
        overviewcontainer = $('<div id="overview-photo-container"></div>');
        overviewbackground = $('<div id="overview-photo-background"></div>');
        overviewcontainer.append(overviewbackground);
        imageId = "horizontal-image";
        errorText.attr('id', 'horizontal-main-photo-error-text');
    }


    //Creates the image itself to be added to the main photo area.
    var detailsImage = $("<img></img>");
    detailsImage.attr("src", photoUrl);
    detailsImage.attr("id", imageId);
    detailsImage.attr("data-photoId", photoId);
    detailsImage.attr("data-photoName", photoName);
    detailsImage.attr("data-photoDesc", photoDesc);
    detailsImage.attr("data-url", photoUrl);
    detailsImage.attr("data-orient", orientation);
    detailsImage.attr("data-photoLike", photoLike);
    detailsImage.css("cursor", "pointer");

    //Sets the click event of the main photo that takes the flow to the vertical or horizontal details area.
    detailsImage.click(function () {
        var likeButton;
        //Sets the information of the details image. Be it Vertical or Horizontal.
        if ($(this).attr("data-orient") === "Vertical") {
            $("#vertical-details-image").attr("src", $(this).attr("data-url"));
            $("#vertical-photo-title").text($(this).attr("data-photoName"));
            $("#vertical-photo-description").text($(this).attr("data-photoDesc"));
            $("#photography-upper-wrapper").attr("data-orient", "Vertical");
            likeButton = $('#vertical-photography-details-container div');
            addGetPhotoLikeCallback(likeButton, photoId, "Vertical");
            changeLikeCount("Vertical", photoId);
            updateLikeButton(likeButton, photoId);
            $("#photography-overview").css("top", "-140vh");  //Gets the main albums area out of the way.
            $("#vertical-photography-details").css("top", "0"); //Brings the vertical details image to the front of the page.
        }
        else if ($(this).attr("data-orient") === "Horizontal") {
            $("#horizontal-details-image").attr("src", $(this).attr("data-url"));
            $("#horizontal-photo-title").text($(this).attr("data-photoName"));
            $("#horizontal-photo-description").text($(this).attr("data-photoDesc"));
            $("#photography-upper-wrapper").attr("data-orient", "Horizontal");
            likeButton = $('#horizontal-photography-details-container div');
            addGetPhotoLikeCallback(likeButton, photoId, "Horizontal");
            changeLikeCount("Horizontal", photoId);
            updateLikeButton($('#horizontal-photography-details-container div'), photoId);
            $("#photography-overview").css("top", "-140vh"); //Gets the main albums area out of the way.
            $("#horizontal-photography-details").css("top", "0"); //Brings the horizontal details image to the front of the page.
        }
        else {
            console.log("Ih!");
        }
        detailsPhotoId = detailsImage.attr('data-photoId');
        $("#return-button").css("display", "inline-block");
        generateCommentPages(detailsPhotoId, $(this).attr('data-orient'));
        setTimeout(function () {
            getLastCommentPageToShow(detailsImage.attr("data-orient"), photoId);
        }, 200);
    });
    overviewbackground.append(detailsImage);

    //Appends the Error container to the image.
    overviewbackground.append(errorText);

    //If user is authenticated, append the like button to the image.
    if (isAuthed) {
        overviewbackground.append(photoLikeDiv);
    }
    detailsContainer.append(overviewcontainer); //Append the photo to the main photo area.
}

//Creates the photos to fill an album's page.
function photosPageClick(event) {
    var pageLink = event.data.pageLink;
    $.ajax({
        url: "/Photo/GetPaginatedPhotos",
        data: { albumId: event.data.albumId, currentPagination: pageLink.attr("data-currPage"), endPagination: pageLink.attr("data-endPage") },
        beforeSend: function () {
            LoaderShowHide();
        },
        success: function (data) {
            LoaderShowHide();

            var thumbsPage = $("#photography-overview .side-content").empty();
            //Add each photo to the album photo page.
            for (var y = 0; y < data.length - 1; y++) {
                var photo = $("<div></div>");
                photo.attr("class", "thumbnails");

                var img = $("<img></img>");
                var path = data[y].thumbnail;
                path = path.slice(1);
                img.attr("src", path);
                img.attr("data-photoId", data[y].photoId);
                img.attr("data-photoUrl", data[y].photopath);
                img.attr("data-orientation", data[y].orientation);
                img.attr("data-photoName", data[y].photoName);
                img.attr("data-photoDesc", data[y].photoDesc);
                img.attr("data-photoLike", data[y].photoLike);

                //Add the main photo when clicking on a thumbnail.
                img.click({ thumbImage: img }, getDetailedPhoto);

                if (data[y].orientation === "Vertical") {
                    img.attr("class", "VerticalThumb");
                    photo.attr("class", "thumbnails VerticalThumbContainer");
                }
                else {
                    img.attr("class", "imageThumb");
                }

                photo.append(img);

                //Adds the delete button if user is authorized to delete it.
                if ($("#photography-upper-wrapper").attr("data-auth") === "True") {
                    var delButton = $('<a class="button del-photo">X</a>');
                    delButton.attr("href", "/Photo/DeletePhoto?photoId=" + data[y].photoId);
                    photo.append(delButton);
                }
                thumbsPage.append(photo);
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            LoaderShowHide();
            if (xhr.status === 404) {
                $('.side-content').empty();
                createErrorContainer($('.side-content'), 'There was a problem retrieving the images. Please try again later or contect the administrator.', 'photos-error-text');
            }
            else {
                $('.side-content').empty();
                createErrorContainer($('.side-content'), 'There was a problem with the request. Please try again later or contact the administrator.', 'photos-error-text');
            }
            console.log("Error: " + errorThrown);
        }
    });
}

//Sets up the settings necessary to work with SignalR in order to update comments in real time.
function signalRSetup() {
    //Assigns the hub to a chat variable
    var chat = $.connection.photoCommentsHub;
    //Sends the comment to be added by the server. Updates the page of the client that sent the request.
    chat.client.sendClient = function (name, message, pageToUpdate) {
        var photoId = $('#photography-upper-wrapper').attr('data-photoId');
        var comment = { PhotoId: photoId, UserName: name, Body: message };
        LoaderShowHide();
        //Sending the Ajax to the server to add the comment.
        $.ajax({
            type: 'POST',
            data: comment,
            url: '/Photo/CreateComment',
            dataType: 'text'
        })
        .done(function () {
            LoaderShowHide();
            //Resets the comment textbox.
            $('#horizontal-comment-expanded-container textarea').val('');
            $('#vertical-comment-expanded-container textarea').val('');
            console.log("Comment generated.");
            //Gets a new set of comments pages in case it has increased with this last comment.
            generateCommentPages(photoId, pageToUpdate);
            setTimeout(function () {
                getLastCommentPageToShow(pageToUpdate, photoId); //SEE IF SETTIMEOUT IS NECESSARY.
                $('#photography-upper-wrapper').attr('data-request-running', false);
                //Enable the sending of comments again.
                $('#horizontal-send-comment').prop('disabled', false);
                $('#vertical-send-comment').prop('disabled', false);
            }, 200);
        })
        .fail(function (xhr) {
            LoaderShowHide();
            console.log("There was an issue creating the comment. Error: " + xhr.status);
            var paginator;
            if (pageToUpdate === "Vertical") {
                paginator = $('#vertical-details-error-text');
                paginator.text('There was an error posting that comment. Please try again later or contact an admin.');
                paginator.show();
            }
            else {
                paginator = $('#horizontal-details-error-text');
                paginator.text('There was an error posting that comment. Please try again later or contact an admin.');
                paginator.show();
            }
            setTimeout(function () {//SEE IF SETTIMEOUT NECESSARY.
                getLastCommentPageToShow(pageToUpdate, photoId);
                $('#photography-upper-wrapper').attr('data-request-running', false);
                $('#horizontal-send-comment').prop('disabled', false);
                $('#vertical-send-comment').prop('disabled', false);
            }, 200);
        });
        $('#photography-upper-wrapper').attr('data-request-running', true);
    };
    //Generates a button on the screen that when clicked, updates the comments section to show that another comment has been added.
    chat.client.sendOthers = function (photoId, pageToUpdate) {
        var overviewPhotoId = parseInt($('#photography-upper-wrapper').attr('data-photoid'));
        console.log('photoid: ' + photoId + '. overviewPhotoId: ' + overviewPhotoId);
        if (photoId === overviewPhotoId) {
            console.log('im in');
            var newComment = getNewCommentLabel(pageToUpdate);
            newComment.click(function () {
                generateCommentPages(photoId, pageToUpdate);
                getLastCommentPageToShow(pageToUpdate, photoId);
            });
            if (pageToUpdate === "Vertical") {
                $('#vertical-side-content').append(newComment);
            }
            else if (pageToUpdate === "Horizontal") {
                $('#horizontal-side-content').append(newComment);
            }
            else {
                console.log("There was a problem with the orientation.");
            }
        }
        else
            console.log('Outra Id. Disregard! Id: ' + photoId);
    };

    $.connection.hub.start().done(function () {
        //Sets the vertical comments send button to send a message to the server hub (which in turn send the message back to the chat to be added).
        $('#vertical-send-comment').click(function () {
            var requestRunning = $('#photography-upper-wrapper').attr('data-request-running');
            if (requestRunning === true) {
                console.log('There is already a request running. Please wait to send another one.');
                return;
            }
            //Create logic for not authed.
            if (isAuthed) {
                if ($('#vertical-comment-expanded-container textarea').val() !== '') {
                    chat.server.send(name, $('#vertical-comment-expanded-container textarea').val(), detailsPhotoId, "Vertical");
                    $('#vertical-send-comment').prop('disabled', true);
                }
                else {
                    alert('Please type your comment in the comment box before pressing send.');
                }
            }
            else {
                //If not authed, error.
                var paginator = $('#vertical-details-error-text');
                paginator.text('You must be logged in in order to post a comment.');
                paginator.show();
            }
            retractVerticalCommentsControls($('#vertical-send-comment'));
        });
        $('#horizontal-send-comment').click(function () {
            var requestRunning = $('#photography-upper-wrapper').attr('data-request-running');
            if (requestRunning === true) {
                return;
            }
            //Create logic for not authed.

            if (isAuthed) {
                if ($('#horizontal-comment-expanded-container textarea').val() !== '') {
                    chat.server.send(name, $('#horizontal-comment-expanded-container textarea').val(), detailsPhotoId, "Horizontal");
                    $('#horizontal-send-comment').prop('disabled', true);
                }
                else {
                    alert('Please type your comment in the comment box before pressing send.');
                }
            }
            else {
                //Create fallback for something not authed.
                var paginator = $('#horizontal-details-error-text');
                paginator.text('You must be logged in in order to post a comment.');
                paginator.show();
            }
            retractHorizontalCommentsControls($('#horizontal-send-comment'));
        });
    })
.fail(function () {
    console.log('connection failed.');
});
}

//Adds the error message when redirecting from main page if something went wrong.
function addOverviewInstructionsError() {
    var overview = $('<div id="overview-instructions-error"></div>');
    var errorTitle = $('<h4>Error:</h4>');
    var errorText = $('<p>The route information necessary to direct you to the content update details was incorrect. ' +
        'Please try again later or contact the administrator.</p>');
    overview.append(errorTitle);
    overview.append(errorText);
    $('#overview-instructions').append(overview);
}

//Sets up the Return Button in the details page to return to index.
function setReturnButton() {
    $("#return-button").click(function () {
        var orientation = $("#photography-upper-wrapper").attr("data-orient");

        //updates the Like button in the details photograph.
        if (orientation === "Vertical") {
            updateLikeButton($("#vertical-photo-background div"), detailsPhotoId);
        }
        else {
            updateLikeButton($("#overview-photo-background div"), detailsPhotoId);
        }

        detailsPhotoId = 'none';
        $("#photography-upper-wrapper").attr("data-orient", "none");

        //returns the flow to the main index area.
        $("#photography-overview").css("top", "0");
        $("#vertical-photography-details").css("top", "110%");
        $("#horizontal-photography-details").css("top", "110%");
        $(this).css("display", "none");
    });
}

//Sets up the textbox controls for the photo comments in the photograph details area.
function setUpCommentTextBoxControls() {

    //Updates the current length of the comment on the max length label. Horizontal area.
    $('#horizontal-comment-expanded-container').on('keyup', 'textarea', function () {
        var textLabel = $('#horizontal-comment-expanded-container label');
        var textBox = $('#horizontal-comment-expanded-container textarea');
        textLabel.text(textBox.val().length + '/ 320');
        if (textBox.val().length > 320) {
            textLabel.css('color', 'red');
        }
        else {
            textLabel.css('color', 'white');
        }
    });

    //Updates the current length of the comment on the max length label. Vertical area.
    $('#vertical-comment-expanded-container').on('keyup', 'textarea', function () {
        var textLabel = $('#vertical-comment-expanded-container label');
        var textBox = $('#vertical-comment-expanded-container textarea');
        textLabel.text(textBox.val().length + '/ 320');
        if (textBox.val().length > 320) {
            textLabel.css('color', 'red');
        }
        else {
            textLabel.css('color', 'white');
        }
    });

    //Expands the horizontal expandable controls when you click inside the Photo comments textbox.
    $('#horizontal-comment-expandable-control').click(function () {
        if (isAuthed) {
            $('#horizontal-comment-expanded-container label').text('0/320');
            $('#horizontal-comment-expandable-container').hide();
            $('#horizontal-comment-expanded-container').css('display', 'flex');
            $('#horizontal-comment-expanded-container textarea').addClass('horizontal-textarea-expanded');
            window.setTimeout(function () {
                $('#horizontal-comment-expanded-container textarea').focus();
            }, 200);

            window.setTimeout(function () {
                $(document).on('click', retractHorizontalCommentsControls);
            }, 200);
        }
            //Warns the user that you need to be authenticated in order to place a comment.
        else {
            var paginator = $('#horizontal-details-error-text');
            paginator.text('You need to be logged in to be able to post comments.');
            paginator.showV();
        }
    });

    //Expands the vertical expandable controls when you click inside the Photo comments textbox.
    $('#vertical-comment-expandable-control').click(function () {
        if (isAuthed) {
            $('#vertical-comment-expanded-container label').text('0/320');
            $('#vertical-comment-expandable-container').hide();
            $('#vertical-comment-expanded-container').css('display', 'flex');
            setTimeout(function () {
                $('#vertical-comment-expanded-container textarea').addClass('vertical-textarea-expanded');
                $('#vertical-comment-expanded-container textarea').focus();
            }, 200);
            setTimeout(function () {
                $(document).on('click', retractVerticalCommentsControls);
            }, 200);
        }
            //Warns the user that you need to be authenticated in order to place a comment.
        else {
            var paginator = $('#vertical-details-error-text');
            paginator.text('You need to be logged in to be able to post comments.');
            paginator.showV();
        }
    });
}

//Redirects from the main page to the photo page.
function setUpRedirectFromMainPage() {
    if ($("#photography-upper-wrapper").attr("data-routeValue") !== undefined) {
        $.ajax({
            url: "/Photo/GetRedirectedPhotoInformation",
            data: { content: $('#photography-upper-wrapper').attr('data-routeValue'), pagination: pagination },
            beforeSend: function () {
                $('#overview-instructions h2').text('The photo you selected is loading.');
                LoaderShowHide();

            },
            success: function (data) {
                LoaderShowHide();
                var albumId = data.albumId;
                console.log("Redirecting to:  Album name - " + data.albumName + ". Album page: " + data.specificPage + ". Photo number: " + data.specificPhoto + ". Photo Id: " + data.photoId + ".");
                generatePhotos(albumId, pagination);
                var pageLink = $('<a></a>');
                pageLink.attr('data-currPage', (data.specificPage - 1) * 8);
                pageLink.attr('data-endPage', (data.specificPage) * 8);
                photosPageClick({ data: { albumId: albumId, pageLink: pageLink } });
                $.getJSON('/Photo/GetPhotoDetails', { photoId: data.photoId })
                .done(function (data) {
                    var img = $("<img></img>");
                    var path = data.thumbnail;
                    path = path.slice(1);
                    img.attr("src", path);
                    img.attr("data-photoId", data.photoId);
                    img.attr("data-photoUrl", data.photopath);
                    img.attr("data-orientation", data.orientation);
                    img.attr("data-photoName", data.photoName);
                    img.attr("data-photoDesc", data.photoDesc);
                    img.attr("data-photoLike", data.photoLike);
                    getDetailedPhoto({ data: { thumbImage: img } });

                })
            .fail(function (xhr) {
                addOverviewInstructionsError();
                console.log("Error: " + xhr.status);
            });
            },
            error: function (event, error, errorThrown) {
                if (event.status === 404) {
                    console.log('The route information necessary to direct you to the content update details were incorrect. Please try again later or contact the administrator.');
                    addOverviewInstructionsError();
                }
                else {
                    console.log('There was an error with this request. Please try again in a few minutes or contact the administrator.' + ' Error code: ' + errorThrown);
                    addOverviewInstructionsError();
                }

            }
        });
    }
}

//Entry point.
(function () {
    $(document).ready(function () {
        //Sets up the button that returns you from the photo details to the main photography index.
        setReturnButton();

        //Sets up the Photo Comments Textbox controls.
        setUpCommentTextBoxControls();

        //SIGNALR setup.
        signalRSetup();

        //Redirects the flow to the specific photo detail page.
        setUpRedirectFromMainPage();
    });
})();