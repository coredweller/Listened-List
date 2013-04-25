<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step1.aspx.cs" Inherits="ListenedList.Step1"
    MasterPageFile="~/Masters/Wooden.Master" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagPrefix="uc" TagName="YearBox" Src="~/Controls/YearBoxes.ascx" %>
<%@ Register TagPrefix="uc" TagName="Legend" Src="~/Controls/Legend.ascx" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $(".lnkFake").click(function (event) {
                window.location = "/Step2";
                return false;
            });

            //If any button on the page is clicked    
            var input = $(":input").click(function (event) {

                //Save the button that was clicked for later to set the new status
                var button = $(this);

                var showDate = $(this).val()

                //if the Search button is clicked do nothing
                if (button.attr('id') == $('#<%= btnSearch.ClientID %>').attr('id')) return false;

                //if the Save button is hit then go to Part 3
                if (button.attr('id') == $('#<%= btnSubmit.ClientID %>').attr('id')) {
                    $(window).scrollTop($('#part3').position().top);
                    return false;
                }

                //Prompt the user for the change in status that they want
                $.prompt('What is the listening status for this show?',

                //Define the 4 buttons to be displayed
                        {buttons: [
                                    { title: 'Finished', value: ListenedStatus.Finished },
                                    { title: 'In Progress', value: ListenedStatus.InProgress },
                                    { title: 'Never Heard', value: ListenedStatus.None },
                                    { title: 'Need To Listen', value: ListenedStatus.NeedToListen },
                                    { title: 'Attended', value: ListenedStatus.Attended },
                                    { title: 'Edit Notes', value: ListenedStatus.EditNotes },
                                    { title: 'Cancel', value: ListenedStatus.Cancel }
                                  ],

                        //This is for Impromptu version 3.2
                        //submit: function (status, y, z) {

                        //This is for Impromptu version 4.0
                        submit: function (x, status, z) {
                            jQuery.prompt.close()

                            //If the user clicks Cancel then do nothing
                            if (status == ListenedStatus.Cancel) { return; }

                            //If the user clicks EditNotes then go to a page to edit the notes
                            if (status == ListenedStatus.EditNotes) {
                                var showInfo = GetPart2ShowText(showDate);

                                //Set show name
                                $('#<%= lblShowName.ClientID %>').html(showInfo.showName);

                                //Set show notes
                                if (FTB_API != null) {
                                    objFTBControl = FTB_API["ctl00_MainContent_txtNotes"];
                                    if (objFTBControl) {
                                        objFTBControl.SetHtml(showInfo.notes);
                                    }
                                }

                                //Move the screen down to Part 2
                                $(window).scrollTop($('#part2').position().top)
                                return;
                            }

                            var attended = (status == ListenedStatus.Attended);

                            //If the button already has the attended border
                            var hasAttended = $(button).hasClass('attendedButton');

                            var cssClass = 0;
                            //Get the color based on the NEW listened status
                            cssClass = GetCssClass(status, attended, hasAttended);

                            //Gets the first half of the class as to get rid of attended
                            var originalClass = $(button).attr('class');

                            //Remove all css classes
                            $(button).removeClass();

                            //Set the button's css class to the new status
                            $(button).addClass(cssClass);

                            if (status == ListenedStatus.Attended) {
                                //If the latest button clicked was attended then add the old color back to the button
                                $(button).addClass(originalClass);

                                //If the latest button clicked was attended but attended was already set then remove it
                                if (hasAttended) {
                                    $(button).removeClass("attendedButton");
                                }
                            }
                            else {
                                if (hasAttended)
                                    $(button).addClass("attendedButton");
                            }

                        }
                    });

                //NEVER remove this
                event.preventDefault();

            });
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
        <div id="content">
        <div style="font-size: 35px; font-weight: bolder; padding-bottom: 25px;">
            Welcome to Phisherman's Guide!
        </div>
        <h3>
            Where phans come to keep track of listening statuses for Phish shows.
        </h3>
        <br />
        <hr />
        <br />
        <h5>
            <%--Whether you listened to the whole show or haven't finished yet, it helps you keep
            track with Notes, Tags, and a simple easy to use button format.
            <br />
            <br />--%>
            <span style="color: rgb(248, 229, 14);">Step 1 shows how to work the buttons, keeping
                notes, and searching notes.
                <br />
                <a href="<%: FriendlyUrl.Href("/Step2") %>">Step 2</a> shows how to create tags,
                alter tags, and how to view your tagged shows. </span>
        </h5>
        <br />
        <br />
        <br />
        <br />
        <h2>
            Part 1: Button Status</h2>
        <ul class="localListItems">
            <li>Click a button and choose your listening status for the show.</li>
            <li>A show can have a listening status and be attended at the same time.</li>
            <li>Click "Edit Notes" to go to Part 2.</li>
        </ul>
        </div>
        <div class="mainDiv">
        <br /><br /><br /><br />
        <uc:Legend ID="legend" runat="server" />
        <br />
        <br />
        <div>
            <uc:YearBox ID="yearBox11" runat="server" Year="1998" Tutorial="true" ShowsToDisplay="4" />
        </div>
        
        <h2 class="tutorialInstructionHeader">
            Part 2: Notes
        </h2>
        <ul class="localListItems">
            <li>Leave notes about where you left off.</li>
            <li>Or how great the show is and the parts that need to be remembered.</li>
            <li>Click Save to go to Part 3</li>
            <%--<li>Everything is 100% searchable, so you can look for any word or phrase easily.</li>--%>
        </ul>
        <br />
        <br />
        <p id="part2" style="font-size: 2em; font-weight: 700;">
            <asp:Label ID="lblShowName" runat="server" Text="4/3/1998 - Nassau Coliseum - Uniondale, NY"></asp:Label>
        </p>
        <br />
        Notes:
        <br />
        <FTB:FreeTextBox ID="txtNotes" runat="server" ToolbarLayout="bold,italic,underline,undo,redo"
            Width="425px" Text="This show is a must listen! You have to check out the Reba and Roses Jam.  Soaring Antelope with Carini's gonna getcha chants!" />
        <br />
        <asp:Button ID="btnSubmit" runat="server" CssClass="normalButton" Text="Save" />
        <br />
        <br />
        <h2 class="tutorialInstructionHeader" style="padding-bottom: 5px;">
            Part 3: Searching Notes</h2>
        <span style="color: Red; font-size: 1.15em;">(Please note: "must listen" is just an
            example provided for you for the purposes of the tutorial.) </span>
        <ul class="localListItems">
            <li>Enter any amount of letters or words into the text box.</li>
            <li>If any notes from any shows have your searched words, links will appear to bring
                you to that show's notes.</li>
            <li>Click a notes link below to go to Step 2.</li>
        </ul>
        <br />
        <br />
        <br />
        <p style="font-size: 1.5em; font-weight: 600;">
            Search Notes:</p>
        <asp:TextBox ID="txtSearch" runat="server" Text="must listen" Enabled="false"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="normalButton" />
        <br />
        <br />
        <br />
        <p style="font-size: 1.5em; font-weight: 600;" id="part3">
            Search Results:</p>
        <br />
        <table style="padding-bottom: 350px;">
            <tr>
                <td>
                    <a class="lnkFake" href="#">This show is a must listen!</a> 4/3/1998
                </td>
            </tr>
            <tr>
                <td>
                    <a class="lnkFake" href="#">must listen to this Drowned</a> 12/31/1995
                </td>
            </tr>
            <tr>
                <td>
                    <a class="lnkFake" href="#">Izabella and Sneakin', must listen to</a> 12/30/97
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
