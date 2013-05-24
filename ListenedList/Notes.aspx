<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Notes.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="ListenedList.Notes" MasterPageFile="~/Masters/Wooden.Master" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#rateItDiv").bind('over', function (event, value) { $(this).attr('title', value); });

            $("#rateItDiv").bind('rated', function (event, value) {

                var listenedId = $('#<%= hdnListenedId.ClientID %>').val();

                $.ajax({
                    type: "GET",
                    url: "/Handlers/RatingHandler.ashx",
                    data: { l: listenedId, r: value },
                });

            });

            $("#rateit5").bind('reset', function () {

                $('#value5').text('Rating reset');

            });
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <p style="font-size: 2em; font-weight: 700;">
            <%= ShowTitle %><br />
            <br />
            <asp:Button ID="btnAttended" runat="server" CssClass="notesDidNotAttend" Text="Did Not Attend"
                OnClick="btnAttended_Click" ToolTip="Click this to change your attended status" /><span
                    style="font-size: 14px;margin-left:200px;"><asp:HyperLink ID="lnkPhishShows" runat="server" Target="_blank"
                        Text="Phishows"></asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkPhishTracks"
                            runat="server" Target="_blank" Text="PhishTracks"></asp:HyperLink></span>
            <asp:HiddenField ID="hdnAttended" runat="server" Value="false" />
        </p>
        <asp:PlaceHolder ID="phSetlist" runat="server" Visible="false">
        <br />
            <div id="divSetlist">
                <p id="lblSetlist" runat="server"></p>
                <%--<asp:Label ID="lblSetlist" runat="server" CssClass="labelFix"></asp:Label>--%>
                <span style="font-size: smaller; display: block;">Courtesy of The Mockingbird Foundation.</span>
                <br />
                <br />
            </div>
        </asp:PlaceHolder>
    </div>
    <div class="mainDiv">
        <br /><br />
        <div style="font-size: 1.5em; font-weight: 600;">
            Listening Status:&nbsp;<%--<asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList>--%>
            <asp:Button ID="btnNeverHeard" runat="server" CssClass="defaultButtonWhite" Text="Never Heard"
                Width="120px" OnClick="btnListenStatus_Click" />
            <asp:Button ID="btnInProgress" runat="server" CssClass="defaultButtonYellow" Text="In Progress"
                Width="120px" OnClick="btnListenStatus_Click" />
            <asp:Button ID="btnFinished" runat="server" CssClass="defaultButtonOrange" Text="Finished"
                Width="120px" OnClick="btnListenStatus_Click" />
            <asp:Button ID="btnNeedToListen" runat="server" CssClass="defaultButtonGreen" Text="Need To Listen"
                Width="120px" OnClick="btnListenStatus_Click" />
            <br />
            <br />
            Rating:<div class="rateit" id="rateItDiv" data-rateit-max="10" data-rateit-value='<%= CurrentRating %>'>
            </div>
            <br />
            <br />
            Notes:
            <br />
            <span id="divSuccess" style="color: Green; font-size: larger; margin-left: 50px;
                display: none; width: auto; line-height: 200px;">NOTES SAVED! </span><span id="divFail"
                    style="color: Red; font-size: larger; margin-left: 50px; display: none; width: auto;
                    line-height: 200px;">NOTES NOT SAVED! Please try again later. </span><span style="float: left;">
                        <FTB:FreeTextBox ID="txtNotes" runat="server" ToolbarLayout="bold,italic,underline,undo,redo"
                            Width="425px" />
                    </span>
            <br />
            <br />
            <p style="font-size: .8em; font-weight: 400; width: 425px;">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="normalButton"
                    Text="Save" />
                <span style="float: right;">First Created:&nbsp;<asp:Label ID="lblCreatedDate" runat="server"></asp:Label><br />
                    Last Updated:&nbsp;<asp:Label ID="lblUpdatedDate" runat="server"></asp:Label></span>
            </p>
            <br />
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p style="font-size: 1.5em; font-weight: 600;">
                    Tags:
                </p>
                <p>
                    Create New Tag (<%= DEFAULT_MAX_TAG_NAME %>&nbsp;letters max):
                    <asp:TextBox ID="txtTagName" runat="server" Width="150px"></asp:TextBox>
                    <asp:Button ID="btnCreateTag" runat="server" Text="Create Tag" CssClass="normalButton"
                        OnClick="btnCreateTag_Click" />
                </p>
                <p>
                    Apply Existing Tag:
                    <asp:DropDownList ID="ddlTags" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnApplyTag" runat="server" OnClick="btnApplyTag_Click" CssClass="normalButton"
                        Text="Apply Tag" />
                </p>
                <div>
                    <asp:Repeater ID="rptTags" runat="server" OnItemCommand="rptTags_ItemCommand">
                        <ItemTemplate>
                            <p>
                                <asp:LinkButton CssClass='<%# (((Data.DomainObjects.ShowTag)Container.DataItem)).Tag.Color %>'
                                    runat="server" Enabled="false" ID="lnkTag" Text='<%# (((Data.DomainObjects.ShowTag)Container.DataItem)).Tag.Name %>'>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Remove" CommandName="DELETE"
                                    CommandArgument='<%# (((Core.DomainObjects.IShowTag)Container.DataItem)).Id %>'></asp:LinkButton>
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCreateTag" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="rptTags" EventName="ItemCommand" />
                <asp:AsyncPostBackTrigger ControlID="btnApplyTag" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <hr />
        <hr />
        <br />
        <br />
        <div>
            <p style="font-size: 1.5em; font-weight: 600;">
                Search Notes:</p>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="normalButton" OnClick="btnSearch_Click" />
            <br />
            <br />
            <br />
            <p style="font-size: 1.5em; font-weight: 600;">
                Search Results:</p>
            <br />
            <asp:Repeater ID="rptNotes" runat="server">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# ((DateTime)Eval("ShowDate")).ToShortDateString() %>
                        </td>
                        <td>
                            <asp:HyperLink runat="server" Text='<%# ShortDescription( (string)Eval( "Notes" ), 30, true ) %>'
                                NavigateUrl='<%# GetUrl((Guid)Eval("ShowId")) %>'></asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <br />
            <asp:Repeater ID="rptPager" runat="server" OnItemCommand="rptPager_ItemCommand">
                <HeaderTemplate>
                    <table>
                        <tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <td>
                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%# (int)Container.DataItem %>'
                            CommandArgument='<%# (int)Container.DataItem %>'></asp:LinkButton>
                    </td>
                </ItemTemplate>
                <FooterTemplate>
                    </tr></table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:HiddenField ID="hdnShowTitle" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnListenedId" runat="server" Visible="true" />
    <asp:HiddenField ID="hdnShowId" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnSearchTerm" runat="server" Visible="false" />
</asp:Content>
