<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="ListenedList.Notes"
    MasterPageFile="~/Masters/Genius.Master" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
       
        $(document).ready(function () {

       //     var input = $("#btnCreateTag").click(function (event) {

            });
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-left: 25px;">
        <asp:HyperLink ID="lnkBack" runat="server" Text="Back to Show Page" NavigateUrl="/Default.aspx"></asp:HyperLink>
    </div>
    <br />
    <br />
    <br />
    <div style="padding-left: 100px;">
        <div style="font-size: 2em; font-weight: 700;">
            <%= ShowTitle %></div>
        <div style="font-size: 1.5em; font-weight: 600;">
            <br />
            <br />
            Notes:
            <br />
            <FTB:FreeTextBox ID="txtNotes" runat="server" ToolbarLayout="bold,italic,underline,undo,redo"
                Width="425px" />
            <%--            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="7" Columns="50"></asp:TextBox>--%>
            <br />
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save Notes" />
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
                    <asp:Button ID="btnCreateTag" runat="server" Text="Create Tag" OnClick="btnCreateTag_Click" />
                </p>
                <p>
                    Apply Existing Tag: <asp:DropDownList ID="ddlTags" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnApplyTag" runat="server" OnClick="btnApplyTag_Click" Text="Apply Tag" />
                </p>
                <div>
                    <asp:Repeater ID="rptTags" runat="server" OnItemCommand="rptTags_ItemCommand">
                        <ItemTemplate>
                            <p>
                                <asp:LinkButton CssClass="tag" runat="server" ID="lnkTag" Text='<%# (((Data.DomainObjects.ShowTag)Container.DataItem)).Tag.Name %>'>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="DELETE" CommandArgument='<%# (((Core.DomainObjects.IShowTag)Container.DataItem)).Id %>'></asp:LinkButton>
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
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
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
