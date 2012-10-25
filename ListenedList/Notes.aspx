<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Notes.aspx.cs"
    Inherits="ListenedList.Notes" MasterPageFile="~/Masters/Genius.Master" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-left: 25px;">
        <a id="lnkBack" href="<%: FriendlyUrl.Href("/Default") %>">Back to Show Page</a>
    </div>
    <br />
    <br />
    <br />
    <div style="padding-left: 100px;">
        <p style="font-size: 2em; font-weight: 700;">
            <%= ShowTitle %>&nbsp;<asp:Button ID="btnAttended" runat="server" CssClass="notesDidNotAttend"
                Text="Did Not Attend" OnClick="btnAttended_Click" />
            <asp:HiddenField ID="hdnAttended" runat="server" Value="false" />
        </p>
        <br />
        <%--<div style="font-size: 1em; font-weight: 400;">
        <br /></div>--%>
        <div style="font-size: 1.5em; font-weight: 600;">
            Listening Status:&nbsp;<%--<asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList>--%>
            <asp:Button ID="btnNeverHeard" runat="server" CssClass="defaultButtonWhite" Text="Never Heard" width="120px" OnClick="btnListenStatus_Click" />
            <asp:Button ID="btnInProgress" runat="server" CssClass="defaultButtonYellow" Text="In Progress" width="120px" OnClick="btnListenStatus_Click" />
            <asp:Button ID="btnFinished" runat="server" CssClass="defaultButtonOrange" Text="Finished" width="120px" OnClick="btnListenStatus_Click" />
            <asp:Button ID="btnNeedToListen" runat="server" CssClass="defaultButtonGreen" Text="Need To Listen" width="120px" OnClick="btnListenStatus_Click" />
            <br />
            <br />
            <br />
            Notes:
            <br />
            <FTB:FreeTextBox ID="txtNotes" runat="server" ToolbarLayout="bold,italic,underline,undo,redo"
                Width="425px" />
            <br />
            <p style="font-size: 1em; font-weight: 400;">
                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label><br />
                <asp:Label ID="lblUpdatedDate" runat="server"></asp:Label>
            </p>
            <br />
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="normalButton" Text="Save" />
        </div>
        <br />
        <br />
        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
        <p style="font-size: 1.5em; font-weight: 600;">
            Tags:
        </p>
        <p>
            Create New Tag (30 letters max):
            <asp:TextBox ID="txtTagName" runat="server" Width="150px"></asp:TextBox>
            <asp:Button ID="btnCreateTag" runat="server" Text="Create Tag" CssClass="normalButton" OnClick="btnCreateTag_Click" />
        </p>
        <p>
            Apply Existing Tag:
            <asp:DropDownList ID="ddlTags" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnApplyTag" runat="server" OnClick="btnApplyTag_Click" CssClass="normalButton" Text="Apply Tag" />
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
        <%--</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCreateTag" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="rptTags" EventName="ItemCommand" />
            </Triggers>
        </asp:UpdatePanel>--%>
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
                            <asp:HyperLink runat="server" Text='<%# ShortDescription( (string)Eval( "Notes" ), 30 ) %>'
                                NavigateUrl='<%# GetUrl((Guid)Eval("ShowId")) %>'></asp:HyperLink>
                            <%# ((DateTime)Eval("ShowDate")).ToShortDateString() %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:HiddenField ID="hdnShowTitle" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnListenedId" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnShowId" runat="server" Visible="false" />
</asp:Content>
