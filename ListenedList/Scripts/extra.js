//Consider moving this to an external JS file
var ListenedStatus = {
    None: 0,
    InProgress: 1,
    Finished: 2,
    NeedToListen: 3,
    EditNotes: 5,
    Attended: 7,
    Cancel: 11
}

//Parses the Url and GETs the "name" value if it exists
function getURLParameter(name) {
    return decodeURIComponent((location.search.match(RegExp("[?|&]" + name + '=(.+?)(&|$)')) || [, null])[1]);
}

//Takes in the status of the listenedShow and whether or not they attended the show 
//Outputs the css classes needed to reflect the user's changed information
function GetCssClass(status, attended) {
    var cssClass = "";

    switch (parseInt(status)) {
        case ListenedStatus.InProgress:
            cssClass = "defaultButtonYellow";
            break;
        case ListenedStatus.Finished:
            cssClass = "defaultButtonOrange";
            break;
        case ListenedStatus.NeedToListen:
            cssClass = "defaultButtonGreen";
            break;
        default:
            //Everything else default to white
            cssClass = "defaultButtonWhite"
            break;
    }

    if (attended == "True" || attended == "true" || attended == true) {
        cssClass = cssClass + " attendedButton";
    }

    return cssClass;
}