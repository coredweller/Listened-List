<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Legend.ascx.cs" Inherits="ListenedList.Controls.Legend" %>
<div style="font-size: 1.5em; font-weight: 600;">
    Legend:&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnLegendNeverHeard" runat="server" CssClass="defaultButtonWhite" Width="110px"
        Enabled="false" Text="Never Heard"></asp:Button>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnLegendInProgress" runat="server" Enabled="false" CssClass="defaultButtonYellow"
        Width="110px" Text="In Progress"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnLegendFinished" runat="server" Enabled="false" CssClass="defaultButtonOrange"
        Width="110px" Text="Finished"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnLegendNeedToListen" runat="server" Enabled="false" CssClass="defaultButtonGreen"
        Width="120px" Text="Need to Listen" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnLegendAttended" runat="server" Enabled="false" CssClass="defaultButtonWhite attendedButton"
        Width="120px" Text="Attended" />
</div>
