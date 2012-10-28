<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotEmail.ascx.cs"
    Inherits="ListenedList.Controls.Templates.ForgotEmail" %>
<style type="text/css">
    h2
    {
        color: #6E6C00;
        font-size: 24px;
        height: 70px;
        line-height: 70px;
        margin: 0;
        padding: 0;
    }
    
    p
    {
        padding: 0 38px 11px 0;
        margin: 0;
    }
</style>
<div>
    <h2>
        Here is your requested information:
    </h2>
    <p>
        User Name:
        <asp:Label ID="lblUserName" runat="server"></asp:Label>
    </p>
    <br />
    <p>
        Password:
        <asp:Label ID="lblPassword" runat="server"></asp:Label>
    </p>
</div>
