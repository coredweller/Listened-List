<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearBoxes.ascx.cs" Inherits="ListenedList.Controls.YearBoxes" %>
<div>
    <asp:Repeater ID="rptShows" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
        </HeaderTemplate>
        <ItemTemplate>
            <td>
                <asp:Button style="text-align:left;" runat="server" Width="65px" BackColor='<%# GetStatus((int)Eval("Status")) %>' Text='<%# ((DateTime)Eval("ShowDate")).ToShortDateString() %>'>
                </asp:Button>
            </td>
        </ItemTemplate>
        <FooterTemplate>
            </tr> </table></FooterTemplate>
    </asp:Repeater>
</div>
