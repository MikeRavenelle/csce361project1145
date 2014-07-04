//Delete a comment from view page
function deleteComment(commentId) {

    var confirm = window.confirm("Please click okay to confirm the removal of your comment");

    if (confirm == true) {

        $('#' + commentId).hide();

        var ajaxData = { commentId: commentId };
        $.ajax({
            url: 'removeComment',
            data: ajaxData,
            dataType: 'json',
            type: "POST",
            async: false,
            success: function () {

            }
        });

    }


}

//Add a comment from view page
function addComment(userId, pictureId) {

    var commentText = document.getElementById(pictureId).value;
    document.getElementById(pictureId).value = "";

    var ajaxUserData = { userId: userId };
    $.ajax({
        url: 'getuser',
        data: ajaxUserData,
        dataType: 'json',
        type: "POST",
        async: false,
        success: function (response) {
            users = response;
        }
    });

    user = users[0];

    var userName = user['userName'];


    var ajaxData = { userId: userId, pictureId: pictureId, commentText: commentText };
    $.ajax({
        url: 'addComment',
        data: ajaxData,
        dataType: 'json',
        type: "POST",
        async: false,
        success: function (response) {
            comment = response;
            curComment = comment[0];

        }
    });

    var contentString = '<tr id="' + curComment['commentId'] + '"><td class="infoWindow_user">' + userName + ": " + '</td>' +
                                                '<td class="infoWindow_comment">' + '<div>' + commentText + '</div> </td>';
        contentString += '<td><button type="button" onclick="deleteComment(' + curComment['commentId'] + ')">Remove</button></td>';


    $('#table' + pictureId).append(contentString);

}


//Delete an image, it's comments, and the location if it is the only image from view page
function deletePicture(pictureId, locationId) {

    var confirm = window.confirm("Please click okay to confirm the removal of your image and all of its comments");

    if (confirm == true) {

        $('#div' + pictureId).hide();

        var ajaxData = { pictureId: pictureId, locationId: locationId };
        $.ajax({
            url: 'removePicture',
            data: ajaxData,
            dataType: 'json',
            type: "POST",
            async: false,
            success: function () {

            }
        });

        var ajaxData = {locationId: locationId };
        $.ajax({
            url: 'getPictures',
            data: ajaxData,
            dataType: 'json',
            type: "POST",
            async: false,
            success: function (response) {
                pictures = response
                if(pictures.length == 0)
                {
                    window.location.reload();
                }
            }
        });
    }
}