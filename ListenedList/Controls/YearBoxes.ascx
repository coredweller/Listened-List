<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearBoxes.ascx.cs" Inherits="ListenedList.Controls.YearBoxes" %>
<div>
    <asp:Repeater ID="rptShows" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <td>
                        <b style="font-size:large;">
                            <%= Year %></b>
                    </td>
        </HeaderTemplate>
        <ItemTemplate>
            <td>
            
             <%--  BorderStyle="Outset" BorderWidth="5px" BorderColor="Purple" --%>
                <asp:Button CssClass='<%# GetCssClass(((Core.Helpers.ShowStatus)Container.DataItem).Status, (((Core.Helpers.ShowStatus)Container.DataItem).Attended)) %>' runat="server" Width="100px" ID="btnYearBox"
                    Text='<%# ((Core.Helpers.ShowStatus)Container.DataItem).ShowDate.ToShortDateString() %>' ToolTip='<%# Eval("ShowName") %>'></asp:Button>
            </td>
        </ItemTemplate>
        <FooterTemplate>
            </tr> </table></FooterTemplate>
    </asp:Repeater>
</div>
