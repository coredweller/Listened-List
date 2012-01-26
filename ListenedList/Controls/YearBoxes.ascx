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
                <asp:Button Style="text-align: left;" runat="server" Width="75px" BackColor='<%# GetStatus((int)Eval("Status")) %>'
                    Text='<%# ((DateTime)Eval("ShowDate")).ToShortDateString() %>'></asp:Button>
            </td>
        </ItemTemplate>
        <FooterTemplate>
            </tr> </table></FooterTemplate>
    </asp:Repeater>
</div>
