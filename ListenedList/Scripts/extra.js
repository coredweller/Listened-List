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
function GetCssClass(status, attended, hasAttended) {
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

    if ((attended == "True" || attended == "true" || attended == true) && !hasAttended) {
        cssClass = cssClass + " attendedButton";
    }

    return cssClass;
}

function GetPart2ShowText(showDate) {
    var showInfo = {};

    switch (showDate) {
        case "4/2/1998":
            showInfo.showName = "4/2/1998 - Nassau Coliseum - Uniondale, NY";
            showInfo.notes = "Wolfman's through Twist was an amazing segment."
            break;
        case "4/4/1998":
            showInfo.showName = "4/4/1998 - Nassau Coliseum - Uniondale, NY";
            showInfo.notes = "Second set is non-stop.  Birds into 2001 into Brother which is a great trio. Ghost, Lizards, Bowie? Wow"
            break;
        case "4/5/1998":
            showInfo.showName = "4/5/1998 - Nassau Coliseum - Uniondale, NY";
            showInfo.notes = "All segues all night.  Unfinished DWD and Maze.  A great jam after Possum to make the night feel complete."
            break;
        case "4/3/1998":
        default:
            showInfo.showName = "4/3/1998 - Nassau Coliseum - Uniondale, NY";
            showInfo.notes = "This show is a must listen! You have to check out the Reba and Roses Jam.  Soaring Antelope with Carini's gonna getcha chants!"
    }

    return showInfo;
}