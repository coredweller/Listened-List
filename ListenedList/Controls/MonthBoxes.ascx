<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonthBoxes.ascx.cs"
    Inherits="ListenedList.Controls.MonthBoxes" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Repeater ID="rptMonth" runat="server">
    <HeaderTemplate>
        <table style="padding: 2px 0px 2px 0px;">
            <tr>
                <td>
                    <a href="<%: FriendlyUrl.Href("~/Main", "Year", Month ) %>" id="lnkMonthMode" class="lnkMonth"
                        style='<%= GetStyle() %>'>
                        <img src="/images/plus_icon.gif" /></a>
                </td>
                <td>
                    <b style="font-size: large;">
                        <%= Month %></b>
                </td>
    </HeaderTemplate>
    <ItemTemplate>
        <td>
            <input type="button" class='<%# GetCssClass(((Core.Helpers.ShowStatus)Container.DataItem).Status, (((Core.Helpers.ShowStatus)Container.DataItem).Attended)) %>'
                id="btnYearBox" style="padding: 5px 0px 5px 0px;font-family:Arial;" value='<%# ((Core.Helpers.ShowStatus)Container.DataItem).ShowDate.ToShortDateString() %>'
                title='<%# ((Core.Helpers.ShowStatus)Container.DataItem).ShowName %>' />
        </td>
    </ItemTemplate>
    <FooterTemplate>
        </tr> </table></FooterTemplate>
</asp:Repeater>
