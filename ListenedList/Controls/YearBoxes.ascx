<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearBoxes.ascx.cs" Inherits="ListenedList.Controls.YearBoxes" %>
<div>
    <asp:Repeater ID="rptShows" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <td>
                        <b style="font-size: large;">
                            <%= Year %></b>
                    </td>
        </HeaderTemplate>
        <ItemTemplate>
            <td>
                <input type="button" class='<%# GetCssClass(((Core.Helpers.ShowStatus)Container.DataItem).Status, (((Core.Helpers.ShowStatus)Container.DataItem).Attended)) %>'
                    id="btnYearBox" style="width: 100px;" value='<%# ((Core.Helpers.ShowStatus)Container.DataItem).ShowDate.ToShortDateString() %>' />
            </td>
        </ItemTemplate>
        <FooterTemplate>
            </tr> </table></FooterTemplate>
    </asp:Repeater>
</div>
