<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Utilities.aspx.cs" Inherits="ListenedList.Admin.Utilities"
    MasterPageFile="~/Masters/Wooden.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Utilities</h1>
    <div>
        <asp:Button ID="btnResetFontSize" runat="server" OnClick="btnResetFontSize_Click" Text="Reset Font Size" />
    </div>
</asp:Content>
