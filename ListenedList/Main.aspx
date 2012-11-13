<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs"
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

            var lastWidth = 100;
            var lastFontSize = 14;

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

                    $(":input[id$=btnYearBox]").css("width", newWidth + "px");
                    $(":input[id$=btnYearBox]").css("font-size", newFontSize + "px");

                    return;
                }
                else if(buttonId == "btnSearch"){
                    var searchedButton = $('input[type="button"][value="' + $('#txtSearch').val() + '"]');
                    if(searchedButton != null) {
                         searchedButton.focus();
                         Blink(searchedButton);
                    }

                    return;
                }
                else if(buttonId == "txtSearch") {
                    return;
                }

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
    <br />
    <br />
    <div style="font-size: 3em; font-weight: 700; padding-bottom: 30px;">
        Phish Shows&nbsp;&nbsp;
        <input id="btnPlus" type="button" class="normalButton plusMinusButton" value="+" />
        <input id="btnMinus" type="button" class="normalButton plusMinusButton" value="-" />&nbsp;&nbsp;
        <span style="font-size: small; font-weight: 200;">Need Help? Tutorial <a href="<%: FriendlyUrl.Href("~/Step1") %>">
            HERE</a></span>
    </div>
    <asp:PlaceHolder ID="phPrivate" runat="server" Visible="false">
        <br />
        <br />
        <div style="font-size: 2.8em; font-weight: 500; color: Red;">
            PROFILE IS PRIVATE
        </div>
        <br />
        <br />
    </asp:PlaceHolder>
    <br />
        Search for Show Date: <input id="txtSearch" type="text" /> <input type="button" id="btnSearch" value="Search" />
    <br />
    <uc:Legend ID="legend" runat="server" />
    <br />
    <%--<br /><br />--%>
    <uc:YearBox ID="yearBox12" runat="server" Year="2012" />
    <br />
    <uc:YearBox ID="yearBox11" runat="server" Year="2011" />
    <br />
    <uc:YearBox ID="yearBox10" runat="server" Year="2010" />
    <br />
    <uc:YearBox ID="yearBox09" runat="server" Year="2009" />
    <br />
    <uc:YearBox ID="yearBox04" runat="server" Year="2004" />
    <br />
    <uc:YearBox ID="yearBox03" runat="server" Year="2003" />
    <br />
    <uc:YearBox ID="yearBox00" runat="server" Year="2000" />
    <br />
    <uc:YearBox ID="yearBox99" runat="server" Year="1999" />
    <br />
    <uc:YearBox ID="yearBox98" runat="server" Year="1998" />
    <br />
    <uc:YearBox ID="yearBox97" runat="server" Year="1997" />
    <br />
    <uc:YearBox ID="yearBox96" runat="server" Year="1996" />
    <br />
    <uc:YearBox ID="yearBox95" runat="server" Year="1995" />
    <br />
    <uc:YearBox ID="yearBox94" runat="server" Year="1994" />
    <br />
    <uc:YearBox ID="yearBox93" runat="server" Year="1993" />
    <br />
    <uc:YearBox ID="yearBox92" runat="server" Year="1992" />
    <br />
    <uc:YearBox ID="yearBox91" runat="server" Year="1991" />
    <br />
    <uc:YearBox ID="yearBox90" runat="server" Year="1990" />
    <br />
    <uc:YearBox ID="yearBox89" runat="server" Year="1989" />
    <br />
    <uc:YearBox ID="yearBox88" runat="server" Year="1988" />
    <br />
    <uc:YearBox ID="yearBox87" runat="server" Year="1987" />
    <br />
    <%--<uc:YearBox ID="yearBox86" runat="server" Year="1986" />
    <br />
    <uc:YearBox ID="yearBox85" runat="server" Year="1985" />
    <br />--%>
    <%--<uc:YearBox ID="yearBox84" runat="server" Year="1984" />
    <br />--%>
    <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
    <div id="dialog-confirm" title="Choose Listening Status">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            What is the listening status for this show?</p>
    </div>
</asp:Content>
