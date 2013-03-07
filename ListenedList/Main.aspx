<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="ListenedList.Main" MasterPageFile="~/Masters/Genius.Master" %>

<%@ Register TagPrefix="uc" TagName="YearBox" Src="~/Controls/YearBoxes.ascx" %>
<%@ Register TagPrefix="uc" TagName="Legend" Src="~/Controls/Legend.ascx" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        //The URL to the notes page
        var notesUrl = "Notes/";

        $(document).ready(function () {
            
            $('#chkAllYears').mousedown(function() {
                var stateBeforeClick = $(this).is(':checked');
                
                if(!stateBeforeClick)
                    window.location.href = "/Main";
                else
                    window.location.href = "/Main/Year/2012/only";

                return;
            });

            //Url parameters
            var pathname = window.location.pathname.split("/");
            SecurityCheck(pathname);

            var lastWidth = '<%= ButtonSize %>';
            var lastFontSize = '<%= FontSize %>';
            var userName = '<%= UserName %>';

            SetButtonProperties(lastWidth, lastFontSize);

            //If any button on the page is clicked    
            var input = $(":input").click(function (event) {

                //grab the showdate from the button that was clicked and replace both slashes with dashes to be more url friendly
                var showDate = $(this).val().replace('/', '-').replace('/', '-');

                //Save the button that was clicked for later to set the new status
                var button = $(this);

                var buttonId = $(button).attr('id');

                if (buttonId == "btnPlus" || buttonId == "btnMinus") {
                    var modifier = buttonId == "btnPlus" ? 1.1 : .9;

                    var newWidth = lastWidth * modifier;
                    var newFontSize = lastFontSize * modifier;

                    lastWidth = newWidth;
                    lastFontSize = newFontSize;

                    $.ajax({
                      type: "GET",
                      url: "/Handlers/ButtonSizeHandler.ashx",
                      data: { width: lastWidth, fontSize: lastFontSize, uName: userName },
                      success: function(msg) {
                        SetButtonProperties(lastWidth, lastFontSize);
                      }
                    });

                    return;
                }
                else if(buttonId == "btnSearch"){
                    //clear errors
                    var error = $("#enterDateError");
                    if(error != null) error.remove();

                    var searched = $('#txtSearch').val();

                    //If the user did not enter anything show an error
                    if ($.trim(searched) == '')
                    {
                        $("#divUtils").append("<span id='enterDateError' style='color:red;'>Please enter a valid date.</span>");
                        return;
                    }
                    
                    //using date.js to parse the date better than normal javascript
                    var parsedDate = Date.parse(searched);

                    //If the user entered a bad date show an error
                    if(parsedDate == null){
                        $("#divUtils").append("<span id='enterDateError' style='color:red;'>Please enter a valid date.</span>");
                        return;
                    }
                    
                    //Format the date into the format the button text is in
                    var finalDate = parsedDate.toString('M/d/yyyy')

                    var searchedButton = $('input[type="button"][value="' + finalDate + '"]');

                    //If a button text equals finalDate then focus on it and make it blink
                    if(searchedButton != null) {
                         searchedButton.focus();
                         Blink(searchedButton);
                    }

                    return;
                }
                else if(buttonId == "txtSearch" || buttonId == "chkAllYears") {
                    return;
                }

                //This makes the button stop blinking
                clearInterval(timer);

                //grab the user id
                var userId = $('#<%= hdnUserId.ClientID %>').val();

                //Do we need to prompt the user?
                var needToPrompt = true;

                //If the button's current color is orange meaning the show is already finished
                if ($(button).hasClass("defaultButtonOrange")) {
                    //Then go to the notes page
                    window.location.href = "/" + notesUrl + showDate;
                    //Dont prompt the user anymore since we are going to the Notes page anyway
                    needToPrompt = false;
                }

                if (needToPrompt) {
                    var modifiedShowName = button.attr("title").replace("-", "<br />").replace("-", "<br />");
                    $("#dialogText").html(modifiedShowName);

                    $("#dialog-confirm").dialog({
                        resizable: true,
                        height: 285,
                        width: 490,
                        modal: true,
                        buttons: {
                            Finished: {
                                text: "Finished",
                                open: function () {
                                    $(this).addClass('defaultButtonOrange');
                                },
                                click: function () {
                                    SaveStatus(ListenedStatus.Finished, showDate, userId, button);
                                    $(this).dialog("close");
                                },
                            },
                            InProgress: {
                                text: "In Progress",
                                open: function () {
                                    $(this).addClass('defaultButtonYellow');
                                },
                                click: function () {
                                    SaveStatus(ListenedStatus.InProgress, showDate, userId, button);
                                    $(this).dialog("close");
                                },
                            },
                            NeverHeard: {
                                text: "Never Heard",
                                open: function () {
                                    $(this).addClass('defaultButtonWhite');
                                },
                                click: function () {
                                    SaveStatus(ListenedStatus.None, showDate, userId, button);
                                    $(this).dialog("close");
                                },
                            },
                            NeedToListen: {
                                text: "Need To Listen",
                                open: function () {
                                    $(this).addClass('defaultButtonGreen');
                                },
                                click: function () {
                                    SaveStatus(ListenedStatus.NeedToListen, showDate, userId, button);
                                    $(this).dialog("close");
                                },
                            },
                            Attended: {
                                text: "Attended",
                                open: function () {
                                    $(this).addClass('attendedButton');
                                },
                                click: function () {
                                    SaveStatus(ListenedStatus.Attended, showDate, userId, button);
                                    $(this).dialog("close");
                                },
                            },
                            EditNotes: {
                                text: "Edit Notes",
                                open: function () {
                                    $(this).addClass('defaultButtonOlive');
                                },
                                click: function () {
                                    SaveStatus(ListenedStatus.EditNotes, showDate, userId, button);
                                    $(this).dialog("close");
                                },
                            },
                            Cancel: {
                                text: "Cancel",
                                priority: 'secondary',
                                click: function () {
                                    $(this).dialog("close");
                                },
                            }
                        }
                    });
                }

                //NEVER remove this
                event.preventDefault();

            });
        });
    </script>
</asp:Content>
<asp:Content ID="cntMain" runat="server" ContentPlaceHolderID="MainContent">
    <div class="mainDiv">
        <br />
        <br />
        <div style="font-size: 3em; font-weight: 700; padding-bottom: 20px;">
            Phish&nbsp;<span style="font-size: small; font-weight: 200;">Need Help? Tutorial <a
                href="<%: FriendlyUrl.Href("~/Step1") %>">HERE</a></span>
        </div>
        <br />
        <asp:PlaceHolder ID="phPrivate" runat="server" Visible="false">
            <br />
            <br />
            <div style="font-size: 2.8em; font-weight: 500; color: Red;">
                PROFILE IS PRIVATE
            </div>
            <br />
            <br />
        </asp:PlaceHolder>
        <div id="divUtils" style="font-size: 18px;">
            Size:
            <input id="btnMinus" type="button" class="normalButton plusMinusButton" value="-" />
            <input id="btnPlus" type="button" class="normalButton plusMinusButton" value="+" />
            &nbsp;&nbsp;&nbsp; Date:
            <input id="txtSearch" type="text" style="width: 100px; height: 32px; font-size: 18px;
                padding-top: 1px;" />
            <input type="button" class="normalButton" id="btnSearch" value="Search" style="height: 38px;" />
        </div>
        <br />
        <br />
        <uc:Legend ID="legend" runat="server" />
        <br />
        <input id="chkAllYears" type="checkbox" checked="checked" />All Years
        <asp:PlaceHolder ID="phYears" runat="server" Visible="false">&nbsp;&nbsp;&nbsp;<a
            href="/Main/Year/2012/only">12</a> <a href="/Main/Year/2011/only">11</a> <a href="/Main/Year/2010/only">
                10</a> <a href="/Main/Year/2009/only">09</a> <a href="/Main/Year/2004/only">04</a>
            <a href="/Main/Year/2003/only">03</a> <a href="/Main/Year/2000/only">00</a> <a href="/Main/Year/1999/only">
                99</a> <a href="/Main/Year/1998/only">98</a> <a href="/Main/Year/1997/only">97</a>
            <a href="/Main/Year/1996/only">96</a> <a href="/Main/Year/1995/only">95</a> <a href="/Main/Year/1994/only">
                94</a> <a href="/Main/Year/1993/only">93</a> <a href="/Main/Year/1992/only">92</a>
            <a href="/Main/Year/1991/only">91</a> <a href="/Main/Year/1990/only">90</a> <a href="/Main/Year/1989/only">
                89</a> <a href="/Main/Year/1988/only">88</a> <a href="/Main/Year/1987/only">87</a>
        </asp:PlaceHolder>
        <br />
        <br />
        <span id="spanYear12">
            <uc:YearBox ID="yearBox12" runat="server" Year="2012" />
        </span><span id="spanYear11">
            <uc:YearBox ID="yearBox11" runat="server" Year="2011" />
        </span><span id="spanYear10">
            <uc:YearBox ID="yearBox10" runat="server" Year="2010" />
        </span><span id="spanYear09">
            <uc:YearBox ID="yearBox09" runat="server" Year="2009" />
        </span><span id="spanYear04">
            <uc:YearBox ID="yearBox04" runat="server" Year="2004" />
        </span><span id="spanYear03">
            <uc:YearBox ID="yearBox03" runat="server" Year="2003" />
        </span><span id="spanYear00">
            <uc:YearBox ID="yearBox00" runat="server" Year="2000" />
        </span><span id="spanYear99">
            <uc:YearBox ID="yearBox99" runat="server" Year="1999" />
        </span><span id="spanYear98">
            <uc:YearBox ID="yearBox98" runat="server" Year="1998" />
        </span><span id="spanYear97">
            <uc:YearBox ID="yearBox97" runat="server" Year="1997" />
        </span><span id="spanYear96">
            <uc:YearBox ID="yearBox96" runat="server" Year="1996" />
        </span><span id="spanYear95">
            <uc:YearBox ID="yearBox95" runat="server" Year="1995" />
        </span><span id="spanYear94">
            <uc:YearBox ID="yearBox94" runat="server" Year="1994" />
        </span><span id="spanYear93">
            <uc:YearBox ID="yearBox93" runat="server" Year="1993" />
        </span><span id="spanYear92">
            <uc:YearBox ID="yearBox92" runat="server" Year="1992" />
        </span><span id="spanYear91">
            <uc:YearBox ID="yearBox91" runat="server" Year="1991" />
        </span><span id="spanYear90">
            <uc:YearBox ID="yearBox90" runat="server" Year="1990" />
        </span><span id="spanYear89">
            <uc:YearBox ID="yearBox89" runat="server" Year="1989" />
        </span><span id="spanYear88">
            <uc:YearBox ID="yearBox88" runat="server" Year="1988" />
        </span><span id="spanYear87">
            <uc:YearBox ID="yearBox87" runat="server" Year="1987" />
        </span>
        <br />
        <br />
        <hr />
        <br />
        <a href='<%: FriendlyUrl.Href("~/Contact") %>'>Contact Us</a>
        <br />
        <br />
        <br />
        Phisherman's Guide is a Coredweller Production
        <br />
        <br />
        <br />
    </div>
    <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
    <div id="dialog-confirm" title="Choose Listening Status for" style="display: none;">
        <p id="dialogText">
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <%--What is the listening status for this show?--%></p>
    </div>
</asp:Content>
