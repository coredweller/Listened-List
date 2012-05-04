﻿<%--<%@ Register TagPrefix="uc" Namespace="ListenedList.Controls" Assembly="ListenedList.Controls" %>--%>
<%@ Register TagPrefix="uc" TagName="YearBox" Src="~/Controls/YearBoxes.ascx" %>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Masters/Genius.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ListenedList._Default" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            //Looks for userName in the URL
            var readOnly = getURLParameter("userName");

            //If a userName is in the URL then disable the buttons
            if (readOnly != "null") {
                $(":input").attr('disabled', true);
                return;
            }

            //If any button on the page is clicked    
            var input = $(":input").click(function (event) {

                //grab the showdate from the button that was clicked
                var showDate = $(this).val();

                //Save the button that was clicked for later to set the new status
                var button = $(this);

                //Do we need to prompt the user?
                var needToPrompt = true;

                //The URL to the notes page
                var notesUrl = "Notes.aspx?showDate=";

                //The RGB value of Orange
                var orangeColor = "defaultButtonOrange";

                //The button's current background color
                var currentColor = $(button).attr("class");

                //If the button's current color is orange meaning the show is already finished
                if (currentColor == orangeColor) {
                    //Then go to the notes page
                    window.location.href = notesUrl + showDate;
                    //Dont prompt the user anymore since we are going to the Notes page anyway
                    needToPrompt = false;
                }

                if (needToPrompt) {

                    //Prompt the user for the change in status that they want
                    $.prompt('What is the listening status for this show?',

                    //Define the 4 buttons to be displayed
                        {buttons: [
                                    { title: 'Finished', value: ListenedStatus.Finished },
                                    { title: 'In Progress', value: ListenedStatus.InProgress },
                                    { title: 'Never Heard', value: ListenedStatus.None },
                                    { title: 'Need To Listen', value: ListenedStatus.NeedToListen },
                                    { title: 'Attended', value: 100 },
                                    { title: 'Edit Notes', value: ListenedStatus.EditNotes },
                                    { title: 'Cancel', value: ListenedStatus.Cancel }
                                  ],

                        //This is for Impromptu version 3.2
                        //submit: function (status, y, z) {

                        //This is for Impromptu version 4.0
                        submit: function (x, status, z) {

                            //If the user clicks Cancel then do nothing
                            if (status == ListenedStatus.Cancel) { return; }

                            //If the user clicks EditNotes then go to a page to edit the notes
                            if (status == ListenedStatus.EditNotes) { window.location.href = notesUrl + showDate; }

                            //grab the user id
                            var userId = $('#<%= hdnUserId.ClientID %>').val();

                            //Send show date, user id, and status to the handler to process the update
                            $.getJSON("Handlers/ShowHandler.ashx", { s: showDate, u: userId, st: status }, function (data) {

                                //If nothing is returned from the Handler then get out of here
                                if (data == null) return;

                                //There should only be one row of data
                                var item = data.records[0];

                                //If there is no data in the json then get out of here
                                if (item == null) return;

                                //Make sure that the Question part of the JSON is success, if it isn't then get out of here
                                if (item['Question'] != "success") return;

                                //If the success was true then set the color
                                if (item['Answer'] == "true") {

                                    var color = 0;
                                    //Get the color based on the NEW listened status
                                    color = GetColor(status);

                                    //Remove all css classes
                                    $(button).removeClass();

                                    //Set the button's css class to the new color
                                    $(button).addClass(color);
                                }

                                //Set the pages focus back on the clicked button, this is so if the button is all the way
                                // to the right the page would refocus there after the user made his choice on the prompt.
                                button.focus();
                            });
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
    <div style="font-size: 3em; font-weight: 700;">
        Phish Shows
    </div>
    <br />
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
    <fieldset>
        <div style="font-size: 1.5em; font-weight: 600;">
            Legend:&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="defaultButtonWhite" Width="110px"
                Enabled="false" Text="Never Heard"></asp:Button>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Enabled="false" CssClass="defaultButtonYellow"
                Width="110px" Text="In Progress"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" Enabled="false" CssClass="defaultButtonOrange"
                Width="110px" Text="Finished"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button4" runat="server" Enabled="false" CssClass="defaultButtonGreen"
                Width="120px" Text="Need to Listen" />
        </div>
    </fieldset>
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
    <uc:YearBox ID="yearBox86" runat="server" Year="1986" />
    <br />
    <uc:YearBox ID="yearBox85" runat="server" Year="1985" />
    <br />
    <%--<uc:YearBox ID="yearBox84" runat="server" Year="1984" />
    <br />--%>
    <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
</asp:Content>
