<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Legend.ascx.cs" Inherits="ListenedList.Controls.Legend" %>
<div style="font-size: 1.5em; font-weight: 600;">
    Legend:&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" CssClass="defaultButtonWhite" Width="110px"
        Enabled="false" Text="Never Heard"></asp:Button>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Enabled="false" CssClass="defaultButtonYellow"
        Width="110px" Text="In Progress"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Enabled="false" CssClass="defaultButtonOrange"
        Width="110px" Text="Finished"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button4" runat="server" Enabled="false" CssClass="defaultButtonGreen"
        Width="120px" Text="Need to Listen" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button5" runat="server" Enabled="false" CssClass="defaultButtonWhite attendedButton"
        Width="120px" Text="Attended" />
</div>
