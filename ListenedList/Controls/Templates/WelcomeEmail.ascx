<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WelcomeEmail.ascx.cs"
    Inherits="ListenedList.Controls.Templates.WelcomeEmail" %>
<div>
    <h2>
        Welcome to Phisherman's Guide!
    </h2>
    <p>
        Here is your sign in information:
    </p>
    <p>
        User Name:
        <asp:Label ID="lblUserName" runat="server"></asp:Label>
    </p>
    <br />
    <p>
        Password:
        <asp:Label ID="lblPassword" runat="server"></asp:Label>
    </p>
    <br />
    <br />
    <br />
    <p>
        If you have any questions or problems contact us at
        <asp:Label ID="lblHelpEmail" runat="server"></asp:Label>
    </p>
</div>
