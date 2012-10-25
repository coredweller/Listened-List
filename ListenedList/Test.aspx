<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="ListenedList.Test" MasterPageFile="~/Masters/Genius.Master" %>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="btnTestSmtpLocalhost" runat="server" Text="Test SMTP localhost" OnClick="btnTestSmtpLocalhost_Click" />
    <asp:Button ID="btnTestSmtpConfig" runat="server" Text="Test SMTP configuration" OnClick="btnTestSmtpConfig_Click" />
    <br />
    <br />
    <asp:Label ID="lblOutput" runat="server"></asp:Label>
</asp:Content>