<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="ListenedList.Notes"
    MasterPageFile="~/Masters/Site.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HyperLink ID="lnkBack" runat="server" Text="Back to Show Page" NavigateUrl="/Default.aspx"></asp:HyperLink>
    </div>
    <br />
    <br />
    <br />
    <div>
        Notes:
        <br />
        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="7" Columns="50"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
    </div>
    <asp:HiddenField ID="hdnListenedId" runat="server" Visible="false" />
</asp:Content>
