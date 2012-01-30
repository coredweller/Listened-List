<%--<%@ Register TagPrefix="uc" Namespace="ListenedList.Controls" Assembly="ListenedList.Controls" %>--%>
<%@ Register TagPrefix="uc" TagName="YearBox" Src="~/Controls/YearBoxes.ascx" %>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Masters/Genius.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ListenedList._Default" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            //If any button on the page is clicked    
            var input = $(":input").click(function (event) {

                //grab the showdate from the button that was clicked
                var showDate = $(this).val();

                //Save the button that was clicked for later to set the new status
                var button = $(this);

                //Prompt the user for the change in status that they want
                $.prompt('What is the listening status for this show?',

                //Define the 4 buttons to be displayed
                        {buttons: [
                                    { title: 'Finished', value: 2 },
                                    { title: 'In Progress', value: 1 },
                                    { title: 'Need To Listen', value: 3 },
                                    { title: 'Reset', value: 0 },
                                    { title: 'Edit Notes', value: 5 },
                                    { title: 'Cancel', value: 11 }
                                  ],

                        //x is the button result
                        submit: function (status, y, z) {

                            //If the user clicks Cancel then do nothing
                            if (status == 11) { return; }

                            //If the user clicks EditNotes then go to a page to edit the notes
                            if (status == 5) { window.location.href = "Notes.aspx?showDate=" + showDate; }

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
                                    color = GetColor(status);

                                    $(button).css("background-color", color);
                                }

                            });
                        }
                    });

                event.preventDefault();
            });
        });

        function GetColor(status) {
            switch (status) {
                case 1:
                    return "Yellow";
                case 2:
                    return "Orange";
                case 3:
                    return "GreenYellow";
            }

            return "White";
        }
    
    </script>
</asp:Content>
<asp:Content ID="cntMain" runat="server" ContentPlaceHolderID="MainContent">
    <%--<asp:PlaceHolder ID="phMain" runat="server"></asp:PlaceHolder>--%>
    <%--<uc:YearBox ID="yearBox97" runat="server" Year="1992" />--%>
    <%--<uc:ShowTextBox runat="server" id="ltxtLabelTextBox" />--%>
    <div style="font-size: 3em; font-weight: 700;">
        Phish Shows
    </div>
    <br />
    <br />
    <fieldset>
        <div style="font-size: 1.5em; font-weight: 600;">
            Legend:&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="textLeft" Width="85px" BackColor='White'
                Enabled="false" Text="Never Heard"></asp:Button>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Width="85px" Enabled="false" BackColor='Yellow'
                Text="In Progress"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" Width="75px" Enabled="false" BackColor='Orange'
                Text="Finished"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button4" runat="server" Width="95px" Enabled="false" BackColor="GreenYellow"
                Text="Need to Listen" />
        </div>
    </fieldset>
    <br />
    <uc:YearBox ID="yearBox83" runat="server" Year="1983" />
    <br />
    <uc:YearBox ID="yearBox84" runat="server" Year="1984" />
    <br />
    <uc:YearBox ID="yearBox85" runat="server" Year="1985" />
    <br />
    <uc:YearBox ID="yearBox86" runat="server" Year="1986" />
    <br />
    <uc:YearBox ID="yearBox87" runat="server" Year="1987" />
    <br />
    <uc:YearBox ID="yearBox88" runat="server" Year="1988" />
    <br />
    <uc:YearBox ID="yearBox89" runat="server" Year="1989" />
    <br />
    <uc:YearBox ID="yearBox90" runat="server" Year="1990" />
    <br />
    <uc:YearBox ID="yearBox91" runat="server" Year="1991" />
    <br />
    <uc:YearBox ID="yearBox92" runat="server" Year="1992" />
    <br />
    <uc:YearBox ID="yearBox93" runat="server" Year="1993" />
    <br />
    <uc:YearBox ID="yearBox94" runat="server" Year="1994" />
    <br />
    <uc:YearBox ID="yearBox95" runat="server" Year="1995" />
    <br />
    <uc:YearBox ID="yearBox96" runat="server" Year="1996" />
    <br />
    <uc:YearBox ID="yearBox97" runat="server" Year="1997" />
    <br />
    <uc:YearBox ID="yearBox98" runat="server" Year="1998" />
    <br />
    <uc:YearBox ID="yearBox99" runat="server" Year="1999" />
    <br />
    <uc:YearBox ID="yearBox00" runat="server" Year="2000" />
    <br />
    <uc:YearBox ID="yearBox03" runat="server" Year="2003" />
    <br />
    <uc:YearBox ID="yearBox04" runat="server" Year="2004" />
    <br />
    <uc:YearBox ID="yearBox09" runat="server" Year="2009" />
    <br />
    <uc:YearBox ID="yearBox10" runat="server" Year="2010" />
    <br />
    <uc:YearBox ID="yearBox11" runat="server" Year="2011" />
    <br />
    <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
</asp:Content>
