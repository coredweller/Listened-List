<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="ListenedList.Test"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="btnTestForgot" runat="server" Text="Test Forgot Email" OnClick="btnTestForgot_Click" />
    <asp:Button ID="btnTestWelcome" runat="server" Text="Test Welcome Email" OnClick="btnTestWelcome_Click" />
    <br />
    <br />
    <asp:Label ID="lblOutput" runat="server"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    <div>
        Control Renderer
        <asp:Button ID="btnWelcomeEmail" runat="server" Text="Show Welcome Email Control"
            OnClick="btnWelcomeEmail_Click" />
        <asp:Button ID="btnForgotEmail" runat="server" Text="Show Forgot Email Control" OnClick="btnForgotEmail_Click" />
        <br />
        <asp:PlaceHolder ID="phControl" runat="server" Visible="true"></asp:PlaceHolder>
    </div>
</asp:Content>
