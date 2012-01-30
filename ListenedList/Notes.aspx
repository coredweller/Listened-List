<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="ListenedList.Notes"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-left:25px;">
        <asp:HyperLink ID="lnkBack" runat="server" Text="Back to Show Page" NavigateUrl="/Default.aspx"></asp:HyperLink>
    </div>
    <br />
    <br />
    <br />
    <div style="padding-left:100px;">
        <div style="font-size: 1.5em; font-weight: 600;">
            Notes:
            <br />
            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="7" Columns="50"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save Notes" />
        </div>
        <br />
        <br />
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
                                NavigateUrl='<%# GetUrl((Guid)Eval("ShowId")) %>'></asp:HyperLink> <%# ((DateTime)Eval("ShowDate")).ToShortDateString() %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:HiddenField ID="hdnListenedId" runat="server" Visible="false" />
</asp:Content>
