//Consider moving this to an external JS file
var ListenedStatus = {
	None: 0,
	InProgress: 1,
	Finished: 2,
	NeedToListen: 3,
	EditNotes: 5,
	Cancel: 11
}

function getURLParameter(name) {
	return decodeURIComponent((location.search.match(RegExp("[?|&]" + name + '=(.+?)(&|$)')) || [, null])[1]);
}

function GetColor(status) {
            switch (status) {
                case ListenedStatus.InProgress:
                    return "defaultButtonYellow";
                case ListenedStatus.Finished:
                    return "defaultButtonOrange";
                case ListenedStatus.NeedToListen:
                    return "defaultButtonGreen";
            }

            //Everything else default to white
            return "defaultButtonWhite";
}