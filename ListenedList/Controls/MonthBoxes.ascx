<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthBoxes.ascx.cs"
    Inherits="ListenedList.Controls.MonthBoxes" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<table style="padding: 2px 0px 2px 0px;">
    <tr>
        <asp:PlaceHolder ID="phPlus" runat="server" Visible="true">
            <td>
                <a href='<%= LinkBuilder.DefaultMainLink(new object[] { "Year", Month } ) %>' id="lnkMonthMode" class="lnkMonth"
                    style='<%= GetStyle() %>'>
                    <img src="/images/plus_icon.gif" alt="" /></a>
            </td>
        </asp:PlaceHolder>
        <td>
            <b style="font-size: large;">
                <%= Month %></b>
        </td>
        <asp:Repeater ID="rptMonth" runat="server">
            <ItemTemplate>
                <td>
                    <input type="button" class='<%# GetCssClass(((Core.Helpers.ShowStatus)Container.DataItem).Status, (((Core.Helpers.ShowStatus)Container.DataItem).Attended)) %>'
                        id="btnYearBox" style="padding: 5px 0px 5px 0px; font-family: Arial;" value='<%# ((Core.Helpers.ShowStatus)Container.DataItem).ShowDate.ToShortDateString() %>'
                        title='<%# Server.HtmlEncode( ((Core.Helpers.ShowStatus)Container.DataItem).ShowName ) %>' />
                </td>
            </ItemTemplate>
        </asp:Repeater>
    </tr>
</table>
