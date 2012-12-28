﻿<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs"
    Inherits="ListenedList.Main" MasterPageFile="~/Masters/Genius.Master" %>

<%@ Register TagPrefix="uc" TagName="YearBox" Src="~/Controls/YearBoxes.ascx" %>
<%@ Register TagPrefix="uc" TagName="Legend" Src="~/Controls/Legend.ascx" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        //The URL to the notes page
        var notesUrl = "Notes/";

        $(document).ready(function () {

            //Looks for userName in the URL
            var pathname = window.location.pathname.split("/");
            var userName = pathname[pathname.length - 1];

            //If a userName is in the URL then disable the buttons
            if (userName.toLowerCase() != "main".toLowerCase()) {
                $(":input").attr('disabled', true);
                return;
            }

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

                    //LEFT OFF HERE
                    $.ajax({
                      type: "GET",
                      url: "Handlers/ButtonSizeHandler.ashx",
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
                else if(buttonId == "txtSearch") {
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
                    window.location.href = notesUrl + showDate;
                    //Dont prompt the user anymore since we are going to the Notes page anyway
                    needToPrompt = false;
                }

                if (needToPrompt) {

                    $("#dialog-confirm").dialog({
                        resizable: true,
                        height: 250,
                        width: 400,
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
        <uc:legend id="legend" runat="server" />
        <br />
        <uc:yearbox id="yearBox12" runat="server" year="2012" />
        <uc:yearbox id="yearBox11" runat="server" year="2011" />
        <uc:yearbox id="yearBox10" runat="server" year="2010" />
        <uc:yearbox id="yearBox09" runat="server" year="2009" />
        <uc:yearbox id="yearBox04" runat="server" year="2004" />
        <uc:yearbox id="yearBox03" runat="server" year="2003" />
        <uc:yearbox id="yearBox00" runat="server" year="2000" />
        <uc:yearbox id="yearBox99" runat="server" year="1999" />
        <uc:yearbox id="yearBox98" runat="server" year="1998" />
        <uc:yearbox id="yearBox97" runat="server" year="1997" />
        <uc:yearbox id="yearBox96" runat="server" year="1996" />
        <uc:yearbox id="yearBox95" runat="server" year="1995" />
        <uc:yearbox id="yearBox94" runat="server" year="1994" />
        <uc:yearbox id="yearBox93" runat="server" year="1993" />
        <uc:yearbox id="yearBox92" runat="server" year="1992" />
        <uc:yearbox id="yearBox91" runat="server" year="1991" />
        <uc:yearbox id="yearBox90" runat="server" year="1990" />
        <uc:yearbox id="yearBox89" runat="server" year="1989" />
        <uc:yearbox id="yearBox88" runat="server" year="1988" />
        <uc:yearbox id="yearBox87" runat="server" year="1987" />
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
    <div id="dialog-confirm" title="Choose Listening Status" style="display: none;">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            What is the listening status for this show?</p>
    </div>
</asp:Content>
